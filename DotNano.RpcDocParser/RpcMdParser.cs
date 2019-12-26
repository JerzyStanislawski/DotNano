using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using DotNano.Shared;
using DotNano.Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DotNano.RpcDocParser
{
    public class RpcMdParser
    {
        enum ParsingState
        {
            NotStarted,
            HeadingParsed,
            DocParsed,
            JsonRequestExpected,
            JsonResponseExpected,
            JsonResponseParsed,
            OptionalParameterParsed
        }
        
        public IEnumerable<RpcCallDocDefinition> Parse(string document)
        {
            var state = ParsingState.NotStarted;
            var temporaryState = ParsingState.NotStarted;
            var mdDoc = Markdig.Markdown.Parse(document);

            RpcCallDocDefinition currentRpcCall = null;
            List<string> currentOptionalParameters = new List<string>();
            
            foreach (var block in mdDoc)
            {
                if (block is HeadingBlock headingBlock)
                {
                    var methodName = ((LiteralInline)headingBlock.Inline.FirstChild).Content.ToString();
                    currentRpcCall = new RpcCallDocDefinition(methodName.Replace(" ", "_"));
                    state = ParsingState.HeadingParsed;
                }

                if (block is ParagraphBlock paragraph)
                {
                    var paragraphText = GetTextFromContainerInline(paragraph.Inline);
                    if (String.IsNullOrEmpty(paragraphText))
                        continue;

                    var lines = paragraphText.Split(Environment.NewLine);
                    foreach (var paragraphLine in lines)
                    {
                        if (state == ParsingState.HeadingParsed)
                        {
                            currentRpcCall.Description = paragraphLine.Trim();
                            state = ParsingState.DocParsed;
                        }
                        else if (state == ParsingState.OptionalParameterParsed)
                        {
                            foreach (var param in currentOptionalParameters)
                            {
                                currentRpcCall.OptionalParameters[param] = paragraphLine;
                            }
                            currentOptionalParameters.Clear();
                            state = temporaryState;
                        }
                        else
                        {
                            if (paragraphLine.StartsWith("Request"))
                                state = ParsingState.JsonRequestExpected;
                            else if (paragraphLine.StartsWith("Response"))
                                state = ParsingState.JsonResponseExpected;
                            else if (paragraphLine.ToLower().StartsWith("optional"))
                            {
                                var optionalInline = (LiteralInline)((EmphasisInline)paragraph.Inline.First()).First();
                                var optionalText = optionalInline.Content.ToString();
                                optionalText = optionalText.Substring("optional ".Length);

                                var optionalParams = optionalText.Split(',');
                                foreach (var optionalParam in optionalParams)
                                {
                                    var trimmedOptionalParam = optionalParam.Trim();
                                    if (trimmedOptionalParam.StartsWith('"') && trimmedOptionalParam.EndsWith('"'))
                                    {
                                        if (state != ParsingState.OptionalParameterParsed)
                                        {
                                            temporaryState = state;
                                            state = ParsingState.OptionalParameterParsed;
                                        }
                                        currentOptionalParameters.Add(trimmedOptionalParam.Trim('"'));
                                    }
                                }
                            }
                        }
                    }
                }

                if (block is ListBlock listBlock && state != ParsingState.DocParsed)
                {
                    foreach (ListItemBlock listItem in listBlock)
                    {
                        var text = GetTextFromContainerInline(((ParagraphBlock)listItem.First()).Inline);
                        var fieldName = Tools.AlphaNumOnly(text.Substring(0, text.IndexOf(' ')));
                        var description = text.Substring(text.IndexOf(' ') + 1);
                        currentRpcCall.OptionalParameters[fieldName] = description;
                    }
                }

                if (block is FencedCodeBlock codeBlock)
                {
                    var json = codeBlock.Lines.Lines.Select(x => x.ToString()).Aggregate((x, y) => $"{x}\n{y}");
                    if (state == ParsingState.DocParsed || state == ParsingState.JsonRequestExpected)
                    {
                        currentRpcCall.JsonRequests.Add(Tools.ToValidJson(json));
                        state = ParsingState.JsonResponseExpected;
                        continue;
                    }

                    if (state == ParsingState.JsonResponseExpected)
                    {
                        currentRpcCall.JsonResponses.Add(Tools.ToValidJson(json));
                        state = ParsingState.JsonResponseParsed;
                    }
                }

                if (block is ThematicBreakBlock)
                {
                    if (currentRpcCall != null && currentRpcCall.JsonRequests.Any() && currentRpcCall.JsonResponses.Any())
                        yield return currentRpcCall;
                }
            }

            yield return currentRpcCall;
        }
        
        private string GetTextFromContainerInline(ContainerInline containerInline)
        {
            var text = "";
            foreach (var child in containerInline)
            {
                if (child is LiteralInline literalInline)
                    text += literalInline.Content.ToString();

                if (child is CodeInline codeInline)
                    text += codeInline.Content.ToString();

                if (child is EmphasisInline emphasisInline)
                {
                    if (emphasisInline.DelimiterChar == '_')
                        continue;
                }
                
                if (child is ContainerInline childContainerInline)
                    text += GetTextFromContainerInline(childContainerInline);
                               
                if (child is LineBreakInline)
                {
                    if (!String.IsNullOrEmpty(text) && !text.EndsWith(Environment.NewLine))
                        text += Environment.NewLine;
                }
            }

            return text;
        }
    }
}
