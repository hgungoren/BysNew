using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Accounts;

namespace ToksozBysNew.Accounts
{
    public class AccountsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AccountsDataSeedContributor(IAccountRepository accountRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _accountRepository = accountRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _accountRepository.InsertAsync(new Account
            (
                id: Guid.Parse("9a558c08-8f12-4645-8c4b-60b8e2095ada"),
                accountCode: "cffafb04becb4682a37eb25c5f3cacc020232a41ccea4be29c",
                accountName: "c4e4d9ebc8904c5cbbb7bc7842121a16ec35418c6ecd4f5195dedbdbb68d84cb8bc3553bccfd414ca02ba842fa00d92f0e6ec77727e042a2806367a16ad9fa78e99900c935fd47bcb93449a60450360082e8f8114cac49f5a361dfbd7deafe4a9ac0dc7b00694069888293eb640f0e6e6e774b4ccfd443bfa4fd4f86a87b90e",
                description: "e5eba29278404f4c8b11e768167021037d97f59fc203405882bc435eb220adb289148b8a32384fc290516a1a81a0d1dfb9e1efd7325f46eea30f88bdd8f1b435f6d7bedad4a24e98861a0164ad9a525af65cdaf4de4d483196cf3652288232da33d90ddec5174cbfa71a7c87a519c866fb9374b214284b16ac340d4d859c299f5ee810ef68e24a17994a9665c97c79cbdc091e5c624b465881cd7da9322de801d1a16072199944c7b25a32276a3a9d6fe8f21ea4552a44e299fb8372c8c2bb87332eeb014f8e40f7b6074cc1f4531b720427b2ce04ed4a03a7e03b6fe345399d70c3931d11034c01b8a906e1b57734d8bc580ecde807429289ac7f91f25d16ddfa75732da4d749aeab5deda8ed40381ea639bf60e1e142c1a2ff5b71da63138352ebd68ea50a4657a4179ce5dc07a94b6d2e74fd5a3c4aa7968a76c8325b22158f69840e88984b00860fcf7dd5ae9a7563267eb26b4c4d69b1035d9b569d5c8601ddddb3640a4e4f91557bfe6651a46cb1ef76c9d6d84ffa8b0d15e209f772b4b25e180fb25e4e7082b640b83ff715d495fa79e63cf246f2a7a786677f9607dc4dee305506de4610999730870ba9c6b7db44551360384f4890b0d4978b7935c83194bc36c8ba4a0e9bf3dae2d823e94b7e9345179ea8427298e41a63623f6fcc9d83eb59d23f4337a3669f76bc85b483f8dd87ceea2546aeaac872a81d71f86b77a2fa9e8d274fa2820832a300637146712ae7a9fea44fdd9dfa314a67b26248ec21da0a9ed94dd19ca843e0456091936667389f7be5435e8bc71ccfd9b175b630d07194b6d446be9f53214de0fcd9fee9dd808c528c4b0b87d645b94585da8666a27c379f094d7c998bbf60a01ef3deccf743c1ae214bd8bf2f9863bb29453b8c76bf2862504edd8d640c411ebfa1a16738b47fb85e463e90407c9e4c765c6530b8d4962963452d93f69f50bb7d60bae39afd5570bc4ebf8cc40ee7ac161a9f1e3d49f5abe64d4990b6fdcb33645952d79d2acf03934eb89bee1d803f008d38f8f7bc32c40d46638beec5b96988a0f637d544c82617410c80882b36cc382997411c49035cfe455dbdeaa84a8f4d0ddbd03b899ba9c2476c8bcde4ec225f1fc064cdec7516a045e9a8d642ddb46725f225c1604fed2e445eaa9b063e7240c2c0e8d1159992f848678fef34f713720eecc1a746f2c601458fb720a0cb96464f13b08a250090fa446dba456ee298e3232339ad5def0f6a42ffab1344ee11480e73ae4ba52f6cac4b0d8a4365bd776da527b3085d30c3df43de92df2e021225908830f693baa7ff4abdbd409124a3815e4557b3826d9cde43e39912e9cd93bb72b018141f7463344643a7d862e7deebd08cc43d4b222178448d93a56a935bc12b52233fe35b632b479d",
                isActive: true
            ));

            await _accountRepository.InsertAsync(new Account
            (
                id: Guid.Parse("c192c8bd-e75e-4a89-9579-cc0facc92dad"),
                accountCode: "cf44e3a3f57b48f6a09cf525ee017d27c82ea02bf3b24b75bd",
                accountName: "1948b0efde36498789de13d48e3ab32546b73eaabbce4ac6ba8059f19f7c42e63a2227e5e30e431b9b79f9b1a673cddfaa4ba855b29748519220ca37e3e0d23a5276dee6b3e84c599140b437d2f5dce17c881464d4f048828f95096a89c7a2ebb45817a3f114402da7de3167088a82d481f2e2306b3b49f59e83758959f281e",
                description: "000d379724cd45b8bbdd3abe5bb7bedbe2bec731487a44cba53d3267c2bab425b46d890c53074c74b5b3619c0d0a13e5f11cb79bfc4346d09d63d82c3b6a2f91162d160eef0947158931b70495e83e4f4f05bc00bba4425eaf096d589081f382383f95d861f14448900bd9a2a96b6ebd4db5c44e10094d53af69ef7eb919fc86ae6f3658ea6949c1925b3a29247470622b94aeee83dd4fa0be35fa5f6ab34074dace35446cec477fbd312492bf6c48027812438e8fee4c498354075bb3c94995f77b9e346c4b4c03aabcc2b20f9441e0fc27f3b7acb44ceeaf6fa576f50788cc23c9236c7a4a43de8715f15814e1c42df542cad696af4cf59fdfcedf660946536a0498cc113a4c09be5e690ddff72f9dca0a8d672f614821a96d78830fbba734acea011e0532462286b7e29854e7e0548a1fe797f526405da78663affe0dbce9921bf348bea345949d3b3b8968283943f5e212f49a20433a94333134bf444ea5ba2db9651a734476a9e785ac8db154755ed43b8c54124723adb60b324ca70a4c05917835d0b444e6b11e91f51d02c39e2b5fae9bfd8746ba844e303a429b4c99ddbe82bcd64e43ad962726c29143f906c307e91681bb40ac83ac667c6a4d6435f18386d783884fd8844d6764846f029cb511d25fea8e475e9f229eaed340eaa53c4931bc4137490387db4c64c79754a866719404605d49abb9a4ca2d9d05357526e6ca95d4d74f2f9e33eb088024dec507b70544d35d45f7a1fd41c33086d428a28099bbc062409799bda2283a4648e5f415368096234b1784ef5753718160bad3401951796941969aef544b56e81c870698e9d049af41328d680fc2cb8af37545f57a2db1d04988a2cd5501831fcf8c0b01d32a228c4c8286a2147e3ddc9acfe8beae43f5cc412e9d5f9d6f3c243a442b60bfb3edcf4e219177bdf34cf9e8e2e70d0c47091e4e969ae46581457c4e1531d84c6a2bcb4e879bd1bf0e7a84a02e855703cc4cc3430694d1de4c279a9df669a8e9a5ba5d4d7dba9ff424e1eb0bf02d7fd3b35e0d40c699f17d1eaf7eaa442a21aaf3de7842e79552177f5d823c88f49cfdf369cc4450ab43bba4be8e515099bd5a83321e4c99b69f883a48759beb4dd12df4c4fa4d1c85054dedc1487fd42d9e408d745d4193ba0f2a8d64cf1adcb1dca68916cd433aa86631a17e9d57582462246d8be445c79e4267b946182385b8a3e11e067a4cd48b12e16df83e0004e1af57af261a4db8a2aa5cd71e12ade541624a94cda249538284d4d54dd3126f18a305089afb4a8385edb0017e6030886d1c3503225f4a4aa8bb42b598622f64f7f3c22a36a744e8856082868e0ef0df367b19c2bd1a4a63b73278e11842cee67aff78e4f05545979f4cf73013d3b2152d525d21ab6e40c1",
                isActive: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}