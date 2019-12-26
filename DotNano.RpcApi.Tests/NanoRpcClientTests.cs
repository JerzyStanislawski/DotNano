using System;
using System.Net.Http;
using System.Numerics;
using DotNano.Shared.DataTypes;
using Xunit;

namespace DotNano.RpcApi.Tests
{
    public class NanoRpcClientTests
    {
        NanoRpcClient _nanoRpcClient;
        public NanoRpcClientTests()
        {
            var httpMessageHandler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(httpMessageHandler); //Remove httpMessageHandler parameter to run live tests based on local node.
            _nanoRpcClient = new NanoRpcClient("localhost", 7076, httpClient);
        }

        [Fact]
        public void AccountBalanceTest()
        {
            var response = _nanoRpcClient.AccountBalance(new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountBlockCountTest()
        {
            var response = _nanoRpcClient.AccountBlockCount(new PublicAddress("nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountGetTest()
        {
            var response = _nanoRpcClient.AccountGet(new HexKey64("3068BB1CA04525BB0E416C485FE6A67FD52540227D267CC8B6E8DA958A7FA039"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountHistoryTest()
        {
            var response = _nanoRpcClient.AccountHistory(new PublicAddress("nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountInfoTest()
        {
            var response = _nanoRpcClient.AccountInfo(new PublicAddress("nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountKeyTest()
        {
            var response = _nanoRpcClient.AccountKey(new PublicAddress("nano_1e5aqegc1jb7qe964u4adzmcezyo6o146zb8hm6dft8tkp79za3sxwjym5rx"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountRepresentativeTest()
        {
            var response = _nanoRpcClient.AccountRepresentative(new PublicAddress("nano_39a73oy5ungrhxy5z5oao1xso4zo7dmgpjd4u74xcrx3r1w6rtazuouw6qfi"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountWeightTest()
        {
            var response = _nanoRpcClient.AccountWeight(new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountsBalancesTest()
        {
            var response = _nanoRpcClient.AccountsBalances(new PublicAddress[0]);
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountsFrontiersTest()
        {
            var response = _nanoRpcClient.AccountsFrontiers(new PublicAddress[0]);
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountsPendingTest()
        {
            var response = _nanoRpcClient.AccountsPending(new PublicAddress[0], 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void ActiveDifficultyTest()
        {
            var response = _nanoRpcClient.ActiveDifficulty();
            Assert.NotNull(response);
        }

        [Fact]
        public void AvailableSupplyTest()
        {
            var response = _nanoRpcClient.AvailableSupply();
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockAccountTest()
        {
            var response = _nanoRpcClient.BlockAccount(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockConfirmTest()
        {
            var response = _nanoRpcClient.BlockConfirm(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockCountTest()
        {
            var response = _nanoRpcClient.BlockCount();
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockCountTypeTest()
        {
            var response = _nanoRpcClient.BlockCountType();
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockCreateTest()
        {
            var response = _nanoRpcClient.BlockCreate("state");
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockHashTest()
        {
            var response = _nanoRpcClient.BlockHash(new BlockContent());
            Assert.NotNull(response);
        }

        [Fact]
        public void BlockInfoTest()
        {
            var response = _nanoRpcClient.BlockInfo(new HexKey64("87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9"));
            Assert.NotNull(response);
        }

        [Fact]
        public void BlocksTest()
        {
            var response = _nanoRpcClient.Blocks(new HexKey64[0]);
            Assert.NotNull(response);
        }

        [Fact]
        public void BlocksInfoTest()
        {
            var response = _nanoRpcClient.BlocksInfo(new HexKey64[0]);
            Assert.NotNull(response);
        }

        [Fact]
        public void BootstrapTest()
        {
            var response = _nanoRpcClient.Bootstrap("::ffff:138.201.94.249", 7075);
            Assert.NotNull(response);
        }

        [Fact]
        public void BootstrapAnyTest()
        {
            var response = _nanoRpcClient.BootstrapAny();
            Assert.NotNull(response);
        }

        [Fact]
        public void BootstrapLazyTest()
        {
            var response = _nanoRpcClient.BootstrapLazy(new HexKey64("FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17"));
            Assert.NotNull(response);
        }

        [Fact]
        public void BootstrapStatusTest()
        {
            var response = _nanoRpcClient.BootstrapStatus();
            Assert.NotNull(response);
        }

        [Fact]
        public void ChainTest()
        {
            var response = _nanoRpcClient.Chain(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void ConfirmationActiveTest()
        {
            var response = _nanoRpcClient.ConfirmationActive();
            Assert.NotNull(response);
        }

        [Fact]
        public void ConfirmationHeightCurrentlyProcessingTest()
        {
            var response = _nanoRpcClient.ConfirmationHeightCurrentlyProcessing();
            Assert.NotNull(response);
        }

        [Fact]
        public void ConfirmationHistoryTest()
        {
            var response = _nanoRpcClient.ConfirmationHistory();
            Assert.NotNull(response);
        }

        [Fact]
        public void ConfirmationInfoTest()
        {
            var response = _nanoRpcClient.ConfirmationInfo(new HexKey64("F8BA8CBE61C679231EB06FA03A0CD7CFBE68746396CBBA169BD9E12725682B44"));
            Assert.NotNull(response);
        }

        [Fact]
        public void ConfirmationQuorumTest()
        {
            var response = _nanoRpcClient.ConfirmationQuorum();
            Assert.NotNull(response);
        }

        [Fact]
        public void DatabaseTxnTrackerTest()
        {
            var response = _nanoRpcClient.DatabaseTxnTracker(1000, 0);
            Assert.NotNull(response);
        }

        [Fact]
        public void DelegatorsTest()
        {
            var response = _nanoRpcClient.Delegators(new PublicAddress("nano_1111111111111111111111111111111111111111111111111117353trpda"));
            Assert.NotNull(response);
        }

        [Fact]
        public void DelegatorsCountTest()
        {
            var response = _nanoRpcClient.DelegatorsCount(new PublicAddress("nano_1111111111111111111111111111111111111111111111111117353trpda"));
            Assert.NotNull(response);
        }

        [Fact]
        public void DeterministicKeyTest()
        {
            var response = _nanoRpcClient.DeterministicKey(new HexKey64("0000000000000000000000000000000000000000000000000000000000000000"), 0);
            Assert.NotNull(response);
        }

        [Fact]
        public void EpochUpgradeTest()
        {
            var response = _nanoRpcClient.EpochUpgrade(1, new HexKey64("0000000000000000000000000000000000000000000000000000000000000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void FrontierCountTest()
        {
            var response = _nanoRpcClient.FrontierCount();
            Assert.NotNull(response);
        }

        [Fact]
        public void FrontiersTest()
        {
            var response = _nanoRpcClient.Frontiers(new PublicAddress("nano_1111111111111111111111111111111111111111111111111111hifc8npp"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void KeepaliveTest()
        {
            var response = _nanoRpcClient.Keepalive("::ffff:192.169.0.1", 1024);
            Assert.NotNull(response);
        }

        [Fact]
        public void KeyCreateTest()
        {
            var response = _nanoRpcClient.KeyCreate();
            Assert.NotNull(response);
        }

        [Fact]
        public void KeyExpandTest()
        {
            var response = _nanoRpcClient.KeyExpand(new HexKey64("781186FB9EF17DB6E3D1056550D9FAE5D5BBADA6A6BC370E4CBB938B1DC71DA3"));
            Assert.NotNull(response);
        }

        [Fact]
        public void LedgerTest()
        {
            var response = _nanoRpcClient.Ledger(new PublicAddress("nano_1111111111111111111111111111111111111111111111111111hifc8npp"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void NodeIdTest()
        {
            var response = _nanoRpcClient.NodeId();
            Assert.NotNull(response);
        }

        [Fact]
        public void NodeIdDeleteTest()
        {
            var response = _nanoRpcClient.NodeIdDelete();
            Assert.NotNull(response);
        }

        [Fact]
        public void PeersTest()
        {
            var response = _nanoRpcClient.Peers();
            Assert.NotNull(response);
        }

        [Fact]
        public void PendingTest()
        {
            var response = _nanoRpcClient.Pending(new PublicAddress("nano_1111111111111111111111111111111111111111111111111117353trpda"));
            Assert.NotNull(response);
        }

        [Fact]
        public void PendingExistsTest()
        {
            var response = _nanoRpcClient.PendingExists(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void ProcessTest()
        {
            var response = _nanoRpcClient.Process(new BlockContent());
            Assert.NotNull(response);
        }

        [Fact]
        public void RepresentativesTest()
        {
            var response = _nanoRpcClient.Representatives();
            Assert.NotNull(response);
        }

        [Fact]
        public void RepresentativesOnlineTest()
        {
            var response = _nanoRpcClient.RepresentativesOnline();
            Assert.NotNull(response);
        }

        [Fact]
        public void RepublishTest()
        {
            var response = _nanoRpcClient.Republish(new HexKey64("A170D51B94E00371ACE76E35AC81DC9405D5D04D4CEBC399AEACE07AE05DD293"));
            Assert.NotNull(response);
        }

        [Fact]
        public void SignTest()
        {
            var response = _nanoRpcClient.Sign();
            Assert.NotNull(response);
        }

        [Fact]
        public void StatsTest()
        {
            var response = _nanoRpcClient.Stats("objects");
            Assert.NotNull(response);
        }

        [Fact]
        public void StatsClearTest()
        {
            var response = _nanoRpcClient.StatsClear();
            Assert.NotNull(response);
        }

        [Fact]
        public void StopTest()
        {
            var response = _nanoRpcClient.Stop();
            Assert.NotNull(response);
        }

        [Fact]
        public void SuccessorsTest()
        {
            var response = _nanoRpcClient.Successors(new HexKey64("991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void ValidateAccountNumberTest()
        {
            var response = _nanoRpcClient.ValidateAccountNumber(new PublicAddress("nano_1111111111111111111111111111111111111111111111111117353trpda"));
            Assert.NotNull(response);
        }

        [Fact]
        public void VersionTest()
        {
            var response = _nanoRpcClient.Version();
            Assert.NotNull(response);
        }

        [Fact]
        public void UncheckedTest()
        {
            var response = _nanoRpcClient.Unchecked(1);
            Assert.NotNull(response);
        }

        [Fact]
        public void UncheckedClearTest()
        {
            var response = _nanoRpcClient.UncheckedClear();
            Assert.NotNull(response);
        }

        [Fact]
        public void UncheckedGetTest()
        {
            var response = _nanoRpcClient.UncheckedGet(new HexKey64("19BF0C268C2D9AED1A8C02E40961B67EA56B1681DE274CD0C50F3DD972F0655C"));
            Assert.NotNull(response);
        }

        [Fact]
        public void UncheckedKeysTest()
        {
            var response = _nanoRpcClient.UncheckedKeys(new HexKey64("19BF0C268C2D9AED1A8C02E40961B67EA56B1681DE274CD0C50F3DD972F0655C"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void UnopenedTest()
        {
            var response = _nanoRpcClient.Unopened(new PublicAddress("nano_1111111111111111111111111111111111111111111111111111hifc8npp"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void UptimeTest()
        {
            var response = _nanoRpcClient.Uptime();
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkCancelTest()
        {
            var response = _nanoRpcClient.WorkCancel(new HexKey64("718CC2121C3E641059BC1C2CFC45666C99E8AE922F7A807B7D07B62C995D79E2"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkGenerateTest()
        {
            var response = _nanoRpcClient.WorkGenerate(new HexKey64("718CC2121C3E641059BC1C2CFC45666C99E8AE922F7A807B7D07B62C995D79E2"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkPeerAddTest()
        {
            var response = _nanoRpcClient.WorkPeerAdd("::ffff:172.17.0.1", 7076);
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkPeersTest()
        {
            var response = _nanoRpcClient.WorkPeers();
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkPeersClearTest()
        {
            var response = _nanoRpcClient.WorkPeersClear();
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkValidateTest()
        {
            var response = _nanoRpcClient.WorkValidate("2bf29ef00786a6bc", new HexKey64("718CC2121C3E641059BC1C2CFC45666C99E8AE922F7A807B7D07B62C995D79E2"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountCreateTest()
        {
            var response = _nanoRpcClient.AccountCreate(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountListTest()
        {
            var response = _nanoRpcClient.AccountList(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountMoveTest()
        {
            var response = _nanoRpcClient.AccountMove(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress[0]);
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountRemoveTest()
        {
            var response = _nanoRpcClient.AccountRemove(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_39a73oy5ungrhxy5z5oao1xso4zo7dmgpjd4u74xcrx3r1w6rtazuouw6qfi"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountRepresentativeSetTest()
        {
            var response = _nanoRpcClient.AccountRepresentativeSet(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_39a73oy5ungrhxy5z5oao1xso4zo7dmgpjd4u74xcrx3r1w6rtazuouw6qfi"), new PublicAddress("nano_16u1uufyoig8777y6r8iqjtrw8sg8maqrm36zzcm95jmbd9i9aj5i8abr8u5"));
            Assert.NotNull(response);
        }

        [Fact]
        public void AccountsCreateTest()
        {
            var response = _nanoRpcClient.AccountsCreate(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), 2);
            Assert.NotNull(response);
        }

        [Fact]
        public void PasswordChangeTest()
        {
            var response = _nanoRpcClient.PasswordChange(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), "test");
            Assert.NotNull(response);
        }

        [Fact]
        public void PasswordEnterTest()
        {
            var response = _nanoRpcClient.PasswordEnter(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), "test");
            Assert.NotNull(response);
        }

        [Fact]
        public void PasswordValidTest()
        {
            var response = _nanoRpcClient.PasswordValid(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void ReceiveTest()
        {
            var response = _nanoRpcClient.Receive(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"), new HexKey64("53EAA25CE28FA0E6D55EA9704B32604A736966255948594D55CBB05267CECD48"));
            Assert.NotNull(response);
        }

        [Fact]
        public void ReceiveMinimumTest()
        {
            var response = _nanoRpcClient.ReceiveMinimum();
            Assert.NotNull(response);
        }

        [Fact]
        public void ReceiveMinimumSetTest()
        {
            var response = _nanoRpcClient.ReceiveMinimumSet(BigInteger.Parse("1000000000000000000000000000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void SearchPendingTest()
        {
            var response = _nanoRpcClient.SearchPending(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void SearchPendingAllTest()
        {
            var response = _nanoRpcClient.SearchPendingAll();
            Assert.NotNull(response);
        }

        [Fact]
        public void SendTest()
        {
            var response = _nanoRpcClient.Send(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"), new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"), 1000000);
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletAddTest()
        {
            var response = _nanoRpcClient.WalletAdd(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new HexKey64("34F0A37AAD20F4A260F0A5B3CB3D7FB50673212263E58A380BC10474BB039CE4"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletAddWatchTest()
        {
            var response = _nanoRpcClient.WalletAddWatch(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress[0]);
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletBalancesTest()
        {
            var response = _nanoRpcClient.WalletBalances(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletChangeSeedTest()
        {
            var response = _nanoRpcClient.WalletChangeSeed(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new HexKey64("74F2B37AAD20F4A260F0A5B3CB3D7FB51673212263E58A380BC10474BB039CEE"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletContainsTest()
        {
            var response = _nanoRpcClient.WalletContains(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletCreateTest()
        {
            var response = _nanoRpcClient.WalletCreate();
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletDestroyTest()
        {
            var response = _nanoRpcClient.WalletDestroy(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletExportTest()
        {
            var response = _nanoRpcClient.WalletExport(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletFrontiersTest()
        {
            var response = _nanoRpcClient.WalletFrontiers(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletHistoryTest()
        {
            var response = _nanoRpcClient.WalletHistory(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletInfoTest()
        {
            var response = _nanoRpcClient.WalletInfo(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletLedgerTest()
        {
            var response = _nanoRpcClient.WalletLedger(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletLockTest()
        {
            var response = _nanoRpcClient.WalletLock(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletLockedTest()
        {
            var response = _nanoRpcClient.WalletLocked(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletPendingTest()
        {
            var response = _nanoRpcClient.WalletPending(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), 1);
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletRepresentativeTest()
        {
            var response = _nanoRpcClient.WalletRepresentative(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletRepresentativeSetTest()
        {
            var response = _nanoRpcClient.WalletRepresentativeSet(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletRepublishTest()
        {
            var response = _nanoRpcClient.WalletRepublish(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), 2);
            Assert.NotNull(response);
        }

        [Fact]
        public void WalletWorkGetTest()
        {
            var response = _nanoRpcClient.WalletWorkGet(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkGetTest()
        {
            var response = _nanoRpcClient.WorkGet(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_1111111111111111111111111111111111111111111111111111hifc8npp"));
            Assert.NotNull(response);
        }

        [Fact]
        public void WorkSetTest()
        {
            var response = _nanoRpcClient.WorkSet(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"), new PublicAddress("nano_1111111111111111111111111111111111111111111111111111hifc8npp"), 0);
            Assert.NotNull(response);
        }

        [Fact]
        public void KraiFromRawTest()
        {
            var response = _nanoRpcClient.KraiFromRaw(BigInteger.Parse("1000000000000000000000000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void KraiToRawTest()
        {
            var response = _nanoRpcClient.KraiToRaw(1);
            Assert.NotNull(response);
        }

        [Fact]
        public void MraiFromRawTest()
        {
            var response = _nanoRpcClient.MraiFromRaw(BigInteger.Parse("1000000000000000000000000000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void MraiToRawTest()
        {
            var response = _nanoRpcClient.MraiToRaw(1);
            Assert.NotNull(response);
        }

        [Fact]
        public void RaiFromRawTest()
        {
            var response = _nanoRpcClient.RaiFromRaw(BigInteger.Parse("1000000000000000000000000"));
            Assert.NotNull(response);
        }

        [Fact]
        public void RaiToRawTest()
        {
            var response = _nanoRpcClient.RaiToRaw(1);
            Assert.NotNull(response);
        }

        [Fact]
        public void PaymentBeginTest()
        {
            var response = _nanoRpcClient.PaymentBegin(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void PaymentEndTest()
        {
            var response = _nanoRpcClient.PaymentEnd(new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"), new HexKey64("FFFD1BAEC8EC20814BBB9059B393051AAA8380F9B5A2E6B2489A277D81789EEE"));
            Assert.NotNull(response);
        }

        [Fact]
        public void PaymentInitTest()
        {
            var response = _nanoRpcClient.PaymentInit(new HexKey64("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F"));
            Assert.NotNull(response);
        }

        [Fact]
        public void PaymentWaitTest()
        {
            var response = _nanoRpcClient.PaymentWait(new PublicAddress("nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"), 1, 1000);
            Assert.NotNull(response);
        }
    }
}