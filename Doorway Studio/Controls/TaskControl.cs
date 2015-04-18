using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Settings;
using Doorway_Studio.Helpers;

namespace Doorway_Studio
{
    public partial class TaskControl : UserControl
    {
        public TaskControl()
        {
            InitializeComponent();
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                generalWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
            }

            this.InitializeUI();
            this.ResetFields();
        }

        private void InitializeUI()
        {
            generalTabPage.Text = View.UILanguageResources.GetString("S0000078");
            generalGroupBox.Text = View.UILanguageResources.GetString("S0000078");
            generalPresetLabel.Text = View.UILanguageResources.GetString("S0000099");
            generalTemplateLabel.Text = View.UILanguageResources.GetString("S0000100");
            generalTextLabel.Text = View.UILanguageResources.GetString("S0000038");
            generalCreateThreadsGroupBox.Text = View.UILanguageResources.GetString("S0000101");
            generalCreateDoorwaysLabel.Text = View.UILanguageResources.GetString("S0000102");
            generalCreateThreadsNumberLabel.Text = View.UILanguageResources.GetString("S0000103");
            generalCreateDorwaysTextLabel.Text = View.UILanguageResources.GetString("S0000104");
            generalImagesGroupBox.Text = View.UILanguageResources.GetString("S0000105");
            generalImageSizeGroupBox.Text = View.UILanguageResources.GetString("S0000106");
            generalImageSizeWidthLabel.Text = View.UILanguageResources.GetString("S0000107");
            generalImageSizeHeightLabel.Text = View.UILanguageResources.GetString("S0000108");
            generalImagesNewLabel.Text = View.UILanguageResources.GetString("S0000109");
            generalImagesNamingGroupBox.Text = View.UILanguageResources.GetString("S0000133");
            generalSaveGroupBox.Text = View.UILanguageResources.GetString("S0000046");
            generalCreateSubFolderCheckBox.Text = View.UILanguageResources.GetString("S0000110");
            generalDoorwaysUrlsGroupBox.Text = View.UILanguageResources.GetString("S0000111");
            generalDatesGroupBox.Text = View.UILanguageResources.GetString("S0000513");

            fileMacrosesTabPage.Text = View.UILanguageResources.GetString("S0000112");
            fileMacrosesActionsGroupBox.Text = View.UILanguageResources.GetString("S0000052");
            fileMacrossFileLabel.Text = View.UILanguageResources.GetString("S0000032");
            fileMacrossLabel.Text = View.UILanguageResources.GetString("S0000113");
            fileMacrossEncodingLabel.Text = View.UILanguageResources.GetString("S0000066");
            fileMacrossTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            fileMacrossAddFileMacrossButton.Text = View.UILanguageResources.GetString("S0000114");
            fileMacrossDeleteFileMacrossButton.Text = View.UILanguageResources.GetString("S0000065");
            fileMacrosesGroupBox.Text = View.UILanguageResources.GetString("S0000171");

            fileMacrossMacrossColumn.HeaderText = View.UILanguageResources.GetString("S0000113");
            fileMacrossPathColumn.HeaderText = View.UILanguageResources.GetString("S0000242");
            fileMacrossEncodingColumn.HeaderText = View.UILanguageResources.GetString("S0000066");
            fileMacrossTypeColumn.HeaderText = View.UILanguageResources.GetString("S0000096");

            keywordsCategoriesTabPage.Text = View.UILanguageResources.GetString("S0000115");
            keywordsGroupBox.Text = View.UILanguageResources.GetString("S0000036");
            keywordsLabel.Text = View.UILanguageResources.GetString("S0000036");
            keywordsUsingGroupBox.Text = View.UILanguageResources.GetString("S0000116");
            keywordsUseSelectLabel.Text = View.UILanguageResources.GetString("S0000117");
            keywordsReorderGroupBox.Text = View.UILanguageResources.GetString("S0000118");
            keywordsReorderKeywordsCheckBox.Text = View.UILanguageResources.GetString("S0000119");
            keywordsReorderWordsCheckBox.Text = View.UILanguageResources.GetString("S0000120");
            keywordsSynonymsGroupBox.Text = View.UILanguageResources.GetString("S0000121");
            keywordsSynonymsCheckBox.Text = View.UILanguageResources.GetString("S0000122");
            keywordsSynonymsLabel.Text = View.UILanguageResources.GetString("S0000121");
            keywordsMergeGroupBox.Text = View.UILanguageResources.GetString("S0000123");
            keywordsMergeCheckBox.Text = View.UILanguageResources.GetString("S0000123");
            keywordsMergeWithLabel.Text = View.UILanguageResources.GetString("S0000124");
            keywordsMergeTypeLabel.Text = View.UILanguageResources.GetString("S0000096");

            categoryGroupBox.Text = View.UILanguageResources.GetString("S0000125");
            categoryUseCheckBox.Text = View.UILanguageResources.GetString("S0000117");
            categoryDynamicGroupBox.Text = View.UILanguageResources.GetString("S0000304");
            categoryDynamicExcludeKeysCheckBox.Text = View.UILanguageResources.GetString("S0000507");
            //categoryDynamicCategoriesLabel.Text = View.UILanguageResources.GetString("S0000125");
            categoryStaticGroupBox.Text = View.UILanguageResources.GetString("S0000127");
            categoryDistributeGroupBox.Text = View.UILanguageResources.GetString("S0000128");

            pagesTabPage.Text = View.UILanguageResources.GetString("S0000021");
            pagesDoorwayTypeGroupBox.Text = View.UILanguageResources.GetString("S0000129");
            pagesDoorwayTypeLabel.Text = View.UILanguageResources.GetString("S0000096");

            staticTabPage.Text = View.UILanguageResources.GetString("S0000127");
            pagesStaticBasicGroupBox.Text = View.UILanguageResources.GetString("S0000130");
            pagesStaticBasicExtLabel.Text = View.UILanguageResources.GetString("S0000131");
            pagesStaticBasicSeparatorLabel.Text = View.UILanguageResources.GetString("S0000132");
            pagesStaticNamesGroupBox.Text = View.UILanguageResources.GetString("S0000133");
            pagesStaticNamesTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            pagesStaticNamesPageLabel.Text = View.UILanguageResources.GetString("S0000134");
            pagesStaticNamesCatLabel.Text = View.UILanguageResources.GetString("S0000135");
            pagesStaticContinueGroupBox.Text = View.UILanguageResources.GetString("S0000136");
            pagesStaticContinuesIndexCheckBox.Text = View.UILanguageResources.GetString("S0000137");
            pagesStaticContinuesIndexLabel.Text = View.UILanguageResources.GetString("S0000056");
            pagesStaticContinuesCatCheckBox.Text = View.UILanguageResources.GetString("S0000138");
            pagesStaticContinuesCatLabel.Text = View.UILanguageResources.GetString("S0000056");
            pagesStaticContinuesKeysOnPageLabel.Text = View.UILanguageResources.GetString("S0000139");

            dynamicTabPage.Text = View.UILanguageResources.GetString("S0000126");
            pagesDynamicNamesGroupBox.Text = View.UILanguageResources.GetString("S0000133");
            pagesDynamicNamesPageLabel.Text = View.UILanguageResources.GetString("S0000134");
            pagesDynamicNamesCatLabel.Text = View.UILanguageResources.GetString("S0000135");
            pagesDynamicNamesStaticPageLabel.Text = View.UILanguageResources.GetString("S0000140");
            pagesDynamicContinuesGroupBox.Text = View.UILanguageResources.GetString("S0000136");
            pagesDynamicContinuesIndexCheckBox.Text = View.UILanguageResources.GetString("S0000137");
            pagesDynamicContinuesIndexNameLabel.Text = View.UILanguageResources.GetString("S0000056");
            pagesDynamicContinuesCatCheckBox.Text = View.UILanguageResources.GetString("S0000138");
            pagesDynamicContinuesCatName1Label.Text = View.UILanguageResources.GetString("S0000056") + " #1";
            pagesDynamicContinuesCatName2Label.Text = View.UILanguageResources.GetString("S0000056") + " #2";
            pagesDynamicContinuesKeysOnPageLabel.Text = View.UILanguageResources.GetString("S0000139");

            pagesStaticPagesGroupBox.Text = View.UILanguageResources.GetString("S0000141");
            pagesStaticPagesCheckBox.Text = View.UILanguageResources.GetString("S0000102");
            pagesStaticPagesIncludeIntoSiteMapCheckBox.Text = View.UILanguageResources.GetString("S0000280");
            siteMapGroupBox.Text = View.UILanguageResources.GetString("S0000142");
            siteMapCheckBox.Text = View.UILanguageResources.GetString("S0000102");
            siteMapTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            siteMapHTMLGroupBox.Text = View.UILanguageResources.GetString("S0000143");
            siteMapHTMLTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            siteMapHTMLNameLabel.Text = View.UILanguageResources.GetString("S0000056");
            siteMapHTMLLinksGroupBox.Text = View.UILanguageResources.GetString("S0000144");
            robotsCreateCheckBox.Text = View.UILanguageResources.GetString("S0000102");
            robotsTypeLabel.Text = View.UILanguageResources.GetString("S0000096");

            textGeneratingTabPage.Text = View.UILanguageResources.GetString("S0000145");
            textGeneratingMethodGroupBox.Text = View.UILanguageResources.GetString("S0000146");
            textGeneratingTextLengthGroupBox.Text = View.UILanguageResources.GetString("S0000147");
            textGeneratingTextLengthLabel.Text = View.UILanguageResources.GetString("S0000148");
            textGeneratingKoPGroupBox.Text = View.UILanguageResources.GetString("S0000149");
            textGeneratingKoPMoreThanOneCheckBox.Text = View.UILanguageResources.GetString("S0000150");
            textGeneratingKoPLabel.Text = View.UILanguageResources.GetString("S0000148");
            textGeneratingKeywordsGroupBox.Text = View.UILanguageResources.GetString("S0000036");
            textGeneratingKeywordsPercentLabel.Text = View.UILanguageResources.GetString("S0000151");
            textGeneratingKeywordsInsertLabel.Text = View.UILanguageResources.GetString("S0000152");
            textGeneratingKeywordsPfEKCheckBox.Text = View.UILanguageResources.GetString("S0000153");
            textGeneratingKeywordsInsertOtherCheckBox.Text = View.UILanguageResources.GetString("S0000154");
            textGeneratingSelectionGroupBox.Text = View.UILanguageResources.GetString("S0000155");
            textGeneratingSelectionKeywordsCheckBox.Text = View.UILanguageResources.GetString("S0000036");
            textGeneratingSelectionPhrasesCheckBox.Text = View.UILanguageResources.GetString("S0000156");
            textGeneratingSelectionLabel.Text = View.UILanguageResources.GetString("S0000155");
            textGeneratingPunctuationGroupBox.Text = View.UILanguageResources.GetString("S0000157");
            textGeneratingPunctuationInsertCheckBox.Text = View.UILanguageResources.GetString("S0000152");
            textGeneratingParagraphsGroupBox.Text = View.UILanguageResources.GetString("S0000158");
            textGeneratingParagraphsCheckBox.Text = View.UILanguageResources.GetString("S0000122");
            textGeneratingChainGraphsGroupBox.Text = View.UILanguageResources.GetString("S0000159");
            textGeneratingChainTAGroupBox.Text = View.UILanguageResources.GetString("S0000160");
            textGeneratingChainTAWordsLabel.Text = View.UILanguageResources.GetString("S0000148");
            textGeneratingChainTAIfLongerLabel.Text = View.UILanguageResources.GetString("S0000161");
            textGeneratingChainTACharsLabel.Text = View.UILanguageResources.GetString("S0000162");
            textGeneratingChainConstructionGroupBox.Text = View.UILanguageResources.GetString("S0000163");
            textGeneratingChainConstructionInsertGroupBox.Text = View.UILanguageResources.GetString("S0000164");
            textGeneratingChainConstructionPunctuationGroupBox.Text = View.UILanguageResources.GetString("S0000157");
            textGeneratingChainConstructionProbabilityCheckBox.Text = View.UILanguageResources.GetString("S0000165");
            textGeneratingSentencesGroupBox.Text = View.UILanguageResources.GetString("S0000166");
            textGeneratingSentencesLengthTypeLabel.Text = View.UILanguageResources.GetString("S0000167");
            textGeneratingSentencesLengthLabel.Text = View.UILanguageResources.GetString("S0000166");
            textGeneratingSentencesWordsLabel.Text = View.UILanguageResources.GetString("S0000148");
            textGeneratingSentencesBigLettersCheckBox.Text = View.UILanguageResources.GetString("S0000168");
            textGenerationMixRandAsIsGroupBox.Text = View.UILanguageResources.GetString("S0000169");
            textGenerationMixRandAsIsTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            textGenerationMixRandAsIsPunctuationGroupBox.Text = View.UILanguageResources.GetString("S0000157");
            textGenerationMixRandAsIsRadiusLabel.Text = View.UILanguageResources.GetString("S0000170");
            textGenerationMixRandAsIsConstructionGroupBox.Text = View.UILanguageResources.GetString("S0000163");
            textGenerationMixRandAsIsConstructionInsertGroupBox.Text = View.UILanguageResources.GetString("S0000164");

            macrosesTabPage.Text = View.UILanguageResources.GetString("S0000171");
            macrosesBlocksGroupBox.Text = View.UILanguageResources.GetString("S0000172");
            macrosesBlockMainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesBlockPageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesMenuBlockMainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesMenuBlockPageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesUserBlock1MainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesUserBlock1PageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesUserBlock2MainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesUserBlock2PageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesUserBlock3MainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesUserBlock3PageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesUserBlock4MainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesUserBlock4PageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesUserBlock5MainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesUserBlock5PageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesUserBlock6MainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesUserBlock6PageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesCatMenuBlockMainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesCatMenuBlockPageLabel.Text = View.UILanguageResources.GetString("S0000174");
            macrosesNetBlockMainLabel.Text = View.UILanguageResources.GetString("S0000173");
            macrosesNetBlockPageLabel.Text = View.UILanguageResources.GetString("S0000174");

            entranceTabPage.Text = View.UILanguageResources.GetString("S0000175");
            entranceGeneralGroupBox.Text = View.UILanguageResources.GetString("S0000078");
            entranceGeneralTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            entranceGeneralInsertTypeLabel.Text = View.UILanguageResources.GetString("S0000176");
            entranceGeneralJSFilesEncriptCheckBox.Text = View.UILanguageResources.GetString("S0000177");
            entranceGeneralAcceptorAdressLabel.Text = View.UILanguageResources.GetString("S0000178");
            entranceRedirectFrameGroupBox.Text = View.UILanguageResources.GetString("S0000179");

            linksSpamTabPage.Text = View.UILanguageResources.GetString("S0000180");
            linksInternalGroupBox.Text = View.UILanguageResources.GetString("S0000181");
            linksInternalRelativeURLsCheckBox.Text = View.UILanguageResources.GetString("S0000305");
            linksInternalCreateСheckBox.Text = View.UILanguageResources.GetString("S0000182");
            linksInternalAnchorsLabel.Text = View.UILanguageResources.GetString("S0000183");
            linksInternalLinksLengthLabel.Text = View.UILanguageResources.GetString("S0000184");
            linksExternalGroupBox.Text = View.UILanguageResources.GetString("S0000185");
            linksExternalUseCheckBox.Text = View.UILanguageResources.GetString("S0000122");
            linksExternalInTextTabPage.Text = View.UILanguageResources.GetString("S0000523");
            linksExternalInTextCheckBox.Text = View.UILanguageResources.GetString("S0000524");
            linksExternalInTextIndexLabel.Text = View.UILanguageResources.GetString("S0000525");
            linksExternalInTextPageLabel.Text = View.UILanguageResources.GetString("S0000526");

            spamCreateCheckBox.Text = View.UILanguageResources.GetString("S0000102");
            spamTypesGroupBox.Text = View.UILanguageResources.GetString("S0000186");
            spamSaveGroupBox.Text = View.UILanguageResources.GetString("S0000282");
            spamSaveCheckBox.Text = View.UILanguageResources.GetString("S0000046");
            spamSavePathLabel.Text = View.UILanguageResources.GetString("S0000242");
            spamSaveEncodingLabel.Text = View.UILanguageResources.GetString("S0000066");

            tagsTabPage.Text = View.UILanguageResources.GetString("S0000517");

            tagsActionsGroupBox.Text = View.UILanguageResources.GetString("S0000052");
            tagsActionsFileLabel.Text = View.UILanguageResources.GetString("S0000032");
            tagsActionsEncodingLabel.Text = View.UILanguageResources.GetString("S0000066");
            tagsActionsAddButton.Text = View.UILanguageResources.GetString("S0000114");
            tagsActionsRemoveButton.Text = View.UILanguageResources.GetString("S0000065");

            tagsGroupBox.Text = View.UILanguageResources.GetString("S0000517");
            tagsFileColumn.HeaderText = View.UILanguageResources.GetString("S0000242");
            tagsEncodingColumn.HeaderText = View.UILanguageResources.GetString("S0000066");

            ftpGeneralGroupBox.Text = View.UILanguageResources.GetString("S0000078");
            ftpGeneralSettingsGroupBox.Text = View.UILanguageResources.GetString("S0000095");
            ftpGeneralUploadCheckBox.Text = View.UILanguageResources.GetString("S0000187");
            ftpGeneralUploadSettingsGroupBox.Text = View.UILanguageResources.GetString("S0000188");
            ftpGeneralUploadSettingsUploadInBackGroundCheckBox.Text = View.UILanguageResources.GetString("S0000189");
            ftpGeneralDeleteAfterUploadСheckBox.Text = View.UILanguageResources.GetString("S0000190");
            ftpGeneralUploadSettingsThreadsPerUploadLabel.Text = View.UILanguageResources.GetString("S0000191");
            ftpGeneralFTPDataGroupBox.Text = View.UILanguageResources.GetString("S0000192");

            ftpServerColumn.HeaderText = View.UILanguageResources.GetString("S0000243");
            ftpAccountColumn.HeaderText = View.UILanguageResources.GetString("S0000244");
            ftpPasswordColumn.HeaderText = View.UILanguageResources.GetString("S0000245");
            ftpRemoteFolderColumn.HeaderText = View.UILanguageResources.GetString("S0000246");

            ftpImportToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000296");
            ftpClearToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000297");

            XrumerGeneralGroupBox.Text = View.UILanguageResources.GetString("S0000078");
            XrumerUseCheckBox.Text = View.UILanguageResources.GetString("S0000122");
            XrumerTextGroupBox.Text = View.UILanguageResources.GetString("S0000038");
            XrumerTextOpenButton.Text = View.UILanguageResources.GetString("S0000074");
            XrumerTemplateGroupBox.Text = View.UILanguageResources.GetString("S0000100");
            XrumerApplyDefaultButton.Text = View.UILanguageResources.GetString("S0000519");
            XrumerTemplateOpenButton.Text = View.UILanguageResources.GetString("S0000074");

            // Настройка выпадающих списков
            generalImagesComboBox.Items.Clear();
            generalImagesComboBox.Items.Add(View.UILanguageResources.GetString("S0000200"));
            generalImagesComboBox.Items.Add(View.UILanguageResources.GetString("S0000201"));
            generalImagesComboBox.Items.Add(View.UILanguageResources.GetString("S0000202"));
            generalImagesComboBox.Items.Add(View.UILanguageResources.GetString("S0000203"));
            generalImagesComboBox.Items.Add(View.UILanguageResources.GetString("S0000204"));
            generalImagesComboBox.Items.Add(View.UILanguageResources.GetString("S0000279"));

            generalImagesNamingComboBox.Items.Clear();
            generalImagesNamingComboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            generalImagesNamingComboBox.Items.Add(View.UILanguageResources.GetString("S0000302"));
            generalImagesNamingComboBox.Items.Add(View.UILanguageResources.GetString("S0000303"));

            generalArchiveComboBox.Items.Clear();
            generalArchiveComboBox.Items.Add(View.UILanguageResources.GetString("S0000205"));
            generalArchiveComboBox.Items.Add("Zip");
            generalArchiveComboBox.Items.Add("Tar.gz");

            generalSubfoldersComboBox.Items.Clear();
            generalSubfoldersComboBox.Items.Add(View.UILanguageResources.GetString("S0000206"));
            generalSubfoldersComboBox.Items.Add(View.UILanguageResources.GetString("S0000207"));
            generalSubfoldersComboBox.Items.Add(View.UILanguageResources.GetString("S0000208"));
            generalSubfoldersComboBox.Items.Add(View.UILanguageResources.GetString("S0000281"));

            fileMacrossTypeComboBox.Items.Clear();
            fileMacrossTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            fileMacrossTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000287"));
            fileMacrossTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000288"));
            fileMacrossTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000300"));

            keywordsUseComboBox.Items.Clear();
            keywordsUseComboBox.Items.Add(View.UILanguageResources.GetString("S0000209"));
            keywordsUseComboBox.Items.Add(View.UILanguageResources.GetString("S0000210"));
            keywordsUseComboBox.Items.Add(View.UILanguageResources.GetString("S0000211"));
            keywordsUseComboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            keywordsUseComboBox.Items.Add(View.UILanguageResources.GetString("S0000287"));

            categoryTypeComboBox.Items.Clear();
            categoryTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000126"));
            categoryTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000127"));

            categoryDistributeComboBox.Items.Clear();
            categoryDistributeComboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            categoryDistributeComboBox.Items.Add(View.UILanguageResources.GetString("S0000213"));

            pagesDoorwayTypeComboBox.Items.Clear();
            pagesDoorwayTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000127"));
            pagesDoorwayTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000214"));
            pagesDoorwayTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000518"));

            pagesStaticNamesTypeComboBox.Items.Clear();
            pagesStaticNamesTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000206"));
            pagesStaticNamesTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000215"));
            pagesStaticNamesTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000501"));
            pagesStaticNamesTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000216"));
            pagesStaticNamesTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000502"));

            siteMapTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000514"));
            siteMapTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000515"));

            siteMapHTMLComboBox.Items.Clear();
            siteMapHTMLComboBox.Items.Add(View.UILanguageResources.GetString("S0000217"));
            siteMapHTMLComboBox.Items.Add(View.UILanguageResources.GetString("S0000218"));

            robotsComboBox.Items.Clear();
            robotsComboBox.Items.Add(View.UILanguageResources.GetString("S0000219"));
            robotsComboBox.Items.Add(View.UILanguageResources.GetString("S0000220"));

            textGeneratingMethodComboBox.Items.Clear();
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000221"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000222"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000223"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000275"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000276"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000224"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000225"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000301"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000503"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000504"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000505"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000506"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000508"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000509"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000510"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000511"));
            textGeneratingMethodComboBox.Items.Add(View.UILanguageResources.GetString("S0000512"));

            textGeneratingKeywordsInsertComboBox.Items.Clear();
            textGeneratingKeywordsInsertComboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            textGeneratingKeywordsInsertComboBox.Items.Add(View.UILanguageResources.GetString("S0000232"));
            textGeneratingKeywordsInsertComboBox.Items.Add(View.UILanguageResources.GetString("S0000251"));
            textGeneratingKeywordsInsertComboBox.Items.Add(View.UILanguageResources.GetString("S0000252"));

            textGeneratingChainTAWordsComboBox.Items.Clear();
            textGeneratingChainTAWordsComboBox.Items.Add(View.UILanguageResources.GetString("S0000226"));
            textGeneratingChainTAWordsComboBox.Items.Add(View.UILanguageResources.GetString("S0000227"));

            textGeneratingChainConstructionComboBox.Items.Clear();
            textGeneratingChainConstructionComboBox.Items.Add(View.UILanguageResources.GetString("S0000228"));
            textGeneratingChainConstructionComboBox.Items.Add(View.UILanguageResources.GetString("S0000229"));

            textGeneratingChainConstructionPunctuationComboBox.Items.Clear();
            textGeneratingChainConstructionPunctuationComboBox.Items.Add(View.UILanguageResources.GetString("S0000230"));
            textGeneratingChainConstructionPunctuationComboBox.Items.Add(View.UILanguageResources.GetString("S0000231"));

            textGeneratingSentencesLengthTypeСomboBox.Items.Clear();
            textGeneratingSentencesLengthTypeСomboBox.Items.Add(View.UILanguageResources.GetString("S0000212"));
            textGeneratingSentencesLengthTypeСomboBox.Items.Add(View.UILanguageResources.GetString("S0000232"));

            textGenerationMixRandAsIsTypeComboBox.Items.Clear();
            textGenerationMixRandAsIsTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000148"));
            textGenerationMixRandAsIsTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000233"));
            textGenerationMixRandAsIsTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000166"));

            textGenerationMixRandAsIsPunctuationComboBox.Items.Clear();
            textGenerationMixRandAsIsPunctuationComboBox.Items.Add(View.UILanguageResources.GetString("S0000230"));
            textGenerationMixRandAsIsPunctuationComboBox.Items.Add(View.UILanguageResources.GetString("S0000231"));

            textGenerationMixRandAsIsConstructionComboBox.Items.Clear();
            textGenerationMixRandAsIsConstructionComboBox.Items.Add(View.UILanguageResources.GetString("S0000228"));
            textGenerationMixRandAsIsConstructionComboBox.Items.Add(View.UILanguageResources.GetString("S0000229"));

            macrosesMainLinkComboBox.Items.Clear();
            macrosesMainLinkComboBox.Items.Add(View.UILanguageResources.GetString("S0000234"));
            macrosesMainLinkComboBox.Items.Add("/");
            macrosesMainLinkComboBox.Items.Add("index");
            macrosesMainLinkComboBox.Items.Add(View.UILanguageResources.GetString("S0000216"));

            entranceGeneralTypeComboBox.Items.Clear();
            entranceGeneralTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000235"));
            entranceGeneralTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000236"));
            entranceGeneralTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000216"));

            entranceGeneralInsertTypeComboBox.Items.Clear();
            entranceGeneralInsertTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000237"));
            entranceGeneralInsertTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000238"));
            entranceGeneralInsertTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000239"));

            entranceGeneralAcceptorAdressComboBox.Items.Clear();
            entranceGeneralAcceptorAdressComboBox.Items.Add(View.UILanguageResources.GetString("S0000127"));
            entranceGeneralAcceptorAdressComboBox.Items.Add(View.UILanguageResources.GetString("S0000126"));

            linksInternalAnchorsСomboBox.Items.Clear();
            linksInternalAnchorsСomboBox.Items.Add(View.UILanguageResources.GetString("S0000036"));
            linksInternalAnchorsСomboBox.Items.Add(View.UILanguageResources.GetString("S0000038"));
            linksInternalAnchorsСomboBox.Items.Add(View.UILanguageResources.GetString("S0000209"));

            spamSaveTypeComboBox.Items.Clear();
            spamSaveTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000283"));
            spamSaveTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000284"));
            spamSaveTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000285"));
            spamSaveTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000286"));

            ftpGeneralUploadTypeComboBox.Items.Clear();
            ftpGeneralUploadTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000240"));
            ftpGeneralUploadTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000241"));

            ftpGeneralUploadSettingsUploadArchiveComboBox.Items.Clear();
            ftpGeneralUploadSettingsUploadArchiveComboBox.Items.Add(View.UILanguageResources.GetString("S0000205"));
            ftpGeneralUploadSettingsUploadArchiveComboBox.Items.Add("Zip");
            ftpGeneralUploadSettingsUploadArchiveComboBox.Items.Add("Tar.gz");


            //Меню
            saveAsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000055");
            loadFromToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000199");
            copyToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000070");
            pasteToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000071");
            selectAllToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000069");
            clearToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000072");
            cutToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000274");
        }

        private void ResetFields()
        {
            generalCreateDoorwaysNumericUpDown.Value = 1;
            generalCreateThreadsNumericUpDown.Value = 1;

            generalImagesComboBox.SelectedIndex = 0;
            generalImageWidthMinNumericUpDown.Value = 100;
            generalImageWidthMaxNumericUpDown.Value = 500;
            generalImageHeigthMinNumericUpDown.Value = 100;
            generalImageHeigthMaxNumericUpDown.Value = 500;
            generalImagesNewNumericUpDown.Value = 25;

            generalImagesNamingComboBox.SelectedIndex = 0;
            generalImagesNamingTextBox.Text = string.Empty;

            generalArchiveComboBox.SelectedIndex = 0;
            generalArchiveTextBox.Text = string.Empty;

            generalSaveTextBox.Text = string.Empty;
            generalCreateSubFolderCheckBox.Checked = true;
            generalSubfoldersComboBox.SelectedIndex = 0;

            generalDoorwaysLinksTextBox.Text = string.Empty;

            generalDatesStartDateTimePicker.Value = DateTime.Now;
            generalDatesEndDateTimePicker.Value = DateTime.Now;

            fileMacrossPathTextBox.Text = string.Empty;
            fileMacrossTextBox.Text = string.Empty;
            fileMacrossEncodingComboBox.SelectedIndex = 0;
            fileMacrossTypeComboBox.SelectedIndex = 0;
            fileMacrossDataGridView.Rows.Clear();

            keywordsUseComboBox.SelectedIndex = 0;
            keywordsUseMinNumericUpDown.Value = 500;
            keywordsUseMaxNumericUpDown.Value = 1000;

            keywordsReorderKeywordsCheckBox.Checked = false;
            keywordsReorderWordsCheckBox.Checked = false;
            keywordsReorderWordsCountNumericUpDown.Value = 25;


            keywordsSynonymsCheckBox.Checked = false;
            keywordsSynonymsNumericUpDown.Value = 25;

            keywordsMergeCheckBox.Checked = false;
            keywordsMergeTypeComboBox.SelectedIndex = 2;
            keywordsMergeNMinNumericUpDown.Value = 1;
            keywordsMergeNMaxNumericUpDown.Value = 1;

            categoryUseCheckBox.Checked = false;
            categoryTypeComboBox.SelectedIndex = 0;
            categoryDynamicCategoriesMinNumericUpDown.Value = 4;
            categoryDynamicCategoriesMaxNumericUpDown.Value = 7;
            categoryStaticCategoriesTextBox.Text = string.Empty;
            categoryDistributeComboBox.SelectedIndex = 0;
            categoryDynamicExcludeKeysCheckBox.Checked = false;

            pagesDoorwayTypeComboBox.SelectedIndex = 0;

            pagesStaticBasicExtTextBox.Text = ".html";
            pagesStaticBasicSeparatorTextBox.Text = "-";
            pagesStaticNamesTypeComboBox.SelectedIndex = 0;

            pagesStaticNamesPageTextBox.Text = "[Name]";
            pagesStaticNamesCatTextBox.Text = "[Name]";
            pagesStaticContinuesIndexCheckBox.Checked = false;
            pagesStaticContinuesIndexTextBox.Text = "[Name]-[N]";
            pagesStaticContinuesCatCheckBox.Checked = false;
            pagesStaticContinuesCatTextBox.Text = "[CName]-[N]";
            pagesStaticContinuesKeysOnPageNumericUpDown.Value = 10;

            pagesDynamicNamesPageTextBox.Text = "page";
            pagesDynamicNamesCatTextBox.Text = "category";
            pagesDynamicContinuesIndexCheckBox.Checked = false;
            pagesDynamicContinuesIndexNameTextBox.Text = "index";
            pagesDynamicContinuesCatCheckBox.Checked = false;
            pagesDynamicContinuesCatName1TextBox.Text = "category";
            pagesDynamicContinuesCatName2TextBox.Text = "page";
            pagesDynamicContinuesKeysOnPageNumericUpDown.Value = 10;

            pagesStaticPagesCheckBox.Checked = false;
            pagesStaticPagesTextBox.Text = string.Empty;
            pagesStaticPagesIncludeIntoSiteMapCheckBox.Checked = false;
            pagesRSSFileName.Text = "feed.xml";

            pagesCreateRSSCheckBox.Checked = false;
            pagesRSSNumericUpDown.Value = 10;

            siteMapCheckBox.Checked = false;
            siteMapTypeComboBox.SelectedIndex = 0;
            siteMapHTMLComboBox.SelectedIndex = 0;
            siteMapHTMLNameTextBox.Text = "map-[N]";
            siteMapHTMLLinksMinNumericUpDown.Value = 50;
            siteMapHTMLLinksMaxNumericUpDown.Value = 100;

            robotsComboBox.SelectedIndex = 0;
            robotsCreateCheckBox.Checked = false;
            robotsComboBox.Enabled = false;
            robotsTextBox.Enabled = false;

            textGeneratingMethodComboBox.SelectedIndex = 0;

            textGeneratingTextLengthMinNumericUpDown.Value = 200;
            textGeneratingTextLengthMaxNumericUpDown.Value = 300;

            textGeneratingKoPMoreThanOneCheckBox.Checked = false;
            textGeneratingKoPMinNumericUpDown.Value = 1;
            textGeneratingKoPMaxNumericUpDown.Value = 3;

            textGeneratingKeywordsPercentMinNumericUpDown.Value = 4;
            textGeneratingKeywordsPercentMaxNumericUpDown.Value = 7;
            textGeneratingKeywordsInsertComboBox.SelectedIndex = 0;
            textGeneratingKeywordsInsertNumericUpDown.Value = 3;
            textGeneratingKeywordsPfEKCheckBox.Checked = false;
            textGeneratingKeywordsInsertOtherCheckBox.Checked = false;
            textGeneratingKeywordsInsertOtherNumericUpDown.Value = 3;

            textGeneratingSelectionKeywordsCheckBox.Checked = false;
            textGeneratingSelectionKeywordsTextBox.Text = "strong b i em";
            textGeneratingSelectionPhrasesCheckBox.Checked = false;
            textGeneratingSelectionPhrasesTextBox.Text = "strong b i em";
            textGeneratingSelectionNumericUpDown.Value = 10;

            textGeneratingPunctuationInsertCheckBox.Checked = false;
            textGeneratingPunctuationInsertMinNumericUpDown.Value = 5;
            textGeneratingPunctuationInsertMaxNumericUpDown.Value = 7;
            textGeneratingSentencesBigLettersCheckBox.Checked = false;
            textGeneratingPunctuationTextBox.Text = ". , ! ? ; : -";

            textGeneratingSentencesLengthTypeСomboBox.SelectedIndex = 0;
            textGeneratingSentencesWordsNumericUpDown.Value = 15;
            textGeneratingSentencesWordsStepNumericUpDown.Value = 3;

            textGeneratingParagraphsCheckBox.Checked = false;
            textGeneratingParagraphsMinNumericUpDown.Value = 1;
            textGeneratingParagraphsMaxNumericUpDown.Value = 3;

            textGeneratingChainTAWordsComboBox.SelectedIndex = 0;
            textGeneratingChainTAIfLongerNumericUpDown.Value = 6;

            textGeneratingChainConstructionComboBox.SelectedIndex = 0;
            textGeneratingChainConstructionInsertMinNumericUpDown.Value = 2;
            textGeneratingChainConstructionInsertMaxNumericUpDown.Value = 5;

            textGeneratingChainConstructionPunctuationComboBox.SelectedIndex = 0;
            textGeneratingChainConstructionProbabilityCheckBox.Checked = false;

            textGenerationMixRandAsIsTypeComboBox.SelectedIndex = 0;
            textGenerationMixRandAsIsPunctuationComboBox.SelectedIndex = 0;

            textGenerationMixRandAsIsRadiusNumericUpDown.Value = 3;
            textGenerationMixRandAsIsConstructionComboBox.SelectedIndex = 0;
            textGenerationMixRandAsIsConstructionInsertMinNumericUpDown.Value = 2;
            textGenerationMixRandAsIsConstructionInsertMaxNumericUpDown.Value = 5;

            macrosesMainLinkComboBox.SelectedIndex = 0;
            macrosesSiteTextBox.Text = string.Empty;
            macrosesTitleTextBox.Text = string.Empty;
            macrosesSiteTextBox.Text = string.Empty;

            entranceGeneralTypeComboBox.SelectedIndex = 0;
            entranceGeneralInsertTypeComboBox.SelectedIndex = 0;
            entranceGeneralAcceptorAdressComboBox.SelectedIndex = 0;
            entranceGeneralAcceptorAdressTextBox.Text = string.Empty;
            entranceGeneralJSFilesEncriptCheckBox.Checked = false;
            entranceRedirectFrameTextBox.Text = string.Empty;
            entranceCounterTextBox.Text = string.Empty;

            linksInternalRelativeURLsCheckBox.Checked = false;
            linksInternalCreateСheckBox.Checked = false;
            linksInternalAnchorsСomboBox.SelectedIndex = 0;
            linksInternalLinksLengthMinNumericUpDown.Value = 1;
            linksInternalLinksLengthMaxNumericUpDown.Value = 3;

            linksExternalUseCheckBox.Checked = false;
            linksExternalTextBox.Text = string.Empty;
            linksExternalInTextCheckBox.Checked = false;
            linksExternalInTextIndexMinNumericUpDown.Value = 0;
            linksExternalInTextIndexMaxNumericUpDown.Value = 1;
            linksExternalInTextPageMinNumericUpDown.Value = 0;
            linksExternalInTextPageMaxNumericUpDown.Value = 1;

            spamCreateCheckBox.Checked = false;
            spamTypesTextBox.Text = "[LINK]\r\n<a href=\"[LINK]\">[KEY]</a>\r\n[URL][LINK][/URL]\r\n[URL=[LINK]][KEY][/URL]";
            spamSaveCheckBox.Checked = false;
            spamSaveTypeComboBox.SelectedIndex = 0;
            spamSavePathTextBox.Text = string.Empty;
            spamSaveEncodingComboBox.SelectedIndex = 0;

            tagsActionsEncodingComboBox.SelectedIndex = 0;

            ftpGeneralUploadCheckBox.Checked = false;
            ftpGeneralUploadTypeComboBox.SelectedIndex = 0;
            ftpGeneralUploadSettingsUploadSaveToTextBox.Text = string.Empty;
            ftpGeneralDeleteAfterUploadСheckBox.Checked = false;
            ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex = 0;
            ftpGeneralUploadSettingsUploadArchiveTextBox.Text = string.Empty;
            ftpGeneralUploadSettingsUploadInBackGroundCheckBox.Checked = false;
            ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Value = 1;
            ftpGeneralSettingsDataGridView.Rows.Clear();

            XrumerUseCheckBox.Checked = false;
            XrumerDirectoryTextBox.Text = string.Empty;
            XrumerTextTextBox.Text = string.Empty;
            XrumerTemplateTextBox.Text = string.Empty;
        }

        #region Properties
        /// <summary>
        /// WorkSpace
        /// </summary>
        public int SelectedWorkSpace
        {
            get
            {
                return generalWSComboBox.SelectedIndex;
            }
            set
            {
                generalWSComboBox.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Preset
        /// </summary>
        public int SelectedPreset
        {
            get
            {
                return generalPresetComboBox.SelectedIndex;
            }
            set
            {
                generalPresetComboBox.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Template
        /// </summary>
        public int SelectedTemplate
        {
            get
            {
                return generalTemplateComboBox.SelectedIndex;
            }
            set
            {
                generalTemplateComboBox.SelectedIndex = value;
            }
        }
        /// <summary>
        /// Text
        /// </summary>
        public int SelectedText
        {
            get
            {
                return generalTextComboBox.SelectedIndex;
            }
            set
            {
                generalTextComboBox.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Keyword
        /// </summary>
        public int SelectedKeyword
        {
            get
            {
                return keywordsComboBox.SelectedIndex;
            }
        }

        #endregion

        #region PublicMethods
        public PresetSettings GetSettings()
        {
            PresetSettings currentPresetSettings = new PresetSettings();

            // Сбор настроек
            // General
            currentPresetSettings.GeneralCreateDoorways = (int) generalCreateDoorwaysNumericUpDown.Value;
            currentPresetSettings.GeneralThreads = (int) generalCreateThreadsNumericUpDown.Value;
            currentPresetSettings.GeneralImageType = generalImagesComboBox.SelectedIndex;
            currentPresetSettings.GeneralImageSizeMinWidth = (int) generalImageWidthMinNumericUpDown.Value;
            currentPresetSettings.GeneralImageSizeMaxWidth = (int) generalImageWidthMaxNumericUpDown.Value;
            currentPresetSettings.GeneralImageSizeMinHeight = (int) generalImageHeigthMinNumericUpDown.Value;
            currentPresetSettings.GeneralImageSizeMaxHeight = (int) generalImageHeigthMaxNumericUpDown.Value;
            currentPresetSettings.GeneralGenerateImagesCount = (int) generalImagesNewNumericUpDown.Value;

            currentPresetSettings.GeneralImageNamingType = generalImagesNamingComboBox.SelectedIndex;
            currentPresetSettings.GeneralImageNamingFile = generalImagesNamingTextBox.Text;

            currentPresetSettings.GeneralArchive = generalArchiveComboBox.SelectedIndex;
            currentPresetSettings.GeneralArchiveName = generalArchiveTextBox.Text;

            if (generalSaveTextBox.Text == string.Empty)
            {
                throw new Exception("Error!");
            }

            if (!generalSaveTextBox.Text.EndsWith("\\"))
            {
                generalSaveTextBox.Text += "\\";
            }

            currentPresetSettings.GeneralSaveTo = generalSaveTextBox.Text;
            currentPresetSettings.GeneralCreateSubFolders = generalCreateSubFolderCheckBox.Checked;
            currentPresetSettings.GeneralSubFoldersType = generalSubfoldersComboBox.SelectedIndex;
            string[] tempStringArray = generalDoorwaysLinksTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStringArray.Length; i++)
            {
                if (!tempStringArray[i].StartsWith("http"))
                {
                    tempStringArray[i] = "http://" + tempStringArray[i];
                }
                if (!tempStringArray[i].EndsWith("/"))
                {
                    tempStringArray[i] += "/";
                }
            }
            currentPresetSettings.GeneralDoorwayUrls = tempStringArray;

            currentPresetSettings.GeneralFileDateStart = generalDatesStartDateTimePicker.Value;
            currentPresetSettings.GeneralFileDateEnd = generalDatesEndDateTimePicker.Value;

            // FileMacroses
            currentPresetSettings.FileMacroses = new FileMacross[fileMacrossDataGridView.Rows.Count];
            for (int i = 0; i < fileMacrossDataGridView.Rows.Count; i++)
            {
                currentPresetSettings.FileMacroses[i] = new FileMacross();
                currentPresetSettings.FileMacroses[i].Macross = fileMacrossDataGridView.Rows[i].Cells[0].Value.ToString();
                currentPresetSettings.FileMacroses[i].File = fileMacrossDataGridView.Rows[i].Cells[1].Value.ToString();
                currentPresetSettings.FileMacroses[i].EncodingType = int.Parse(fileMacrossDataGridView.Rows[i].Cells[2].Value.ToString());
                currentPresetSettings.FileMacroses[i].Type = int.Parse(fileMacrossDataGridView.Rows[i].Cells[3].Value.ToString());
            }

            // Keywords/Categories
            //currentPresetSettings.KeywordsID = keywordsComboBox.SelectedIndex;
            currentPresetSettings.KeywordsID = SharedData.WorkSpaces[SelectedWorkSpace].Keywords[keywordsComboBox.SelectedIndex].ID;

            currentPresetSettings.KeywordsSelectType = keywordsUseComboBox.SelectedIndex;
            currentPresetSettings.KeywordsSelectMin = (int) keywordsUseMinNumericUpDown.Value;
            currentPresetSettings.KeywordsSelectMax = (int) keywordsUseMaxNumericUpDown.Value;
            currentPresetSettings.KeywordsReorder = keywordsReorderKeywordsCheckBox.Checked;
            currentPresetSettings.KeywordsWordsReorder = keywordsReorderWordsCheckBox.Checked;
            currentPresetSettings.KeywordsWordsReorderPercentage = (int) keywordsReorderWordsCountNumericUpDown.Value;
            currentPresetSettings.KeywordsSynonyms = keywordsSynonymsCheckBox.Checked;
            currentPresetSettings.KeywordsSynonymsID = keywordsSynonymsComboBox.SelectedIndex;
            currentPresetSettings.KeywordsSynonymsPercentage = (int) keywordsSynonymsNumericUpDown.Value;
            currentPresetSettings.KeywordsMerge = keywordsMergeCheckBox.Checked;
            currentPresetSettings.KeywordsMergeID = keywordsMergeComboBox.SelectedIndex;
            currentPresetSettings.KeywordsMergeType = keywordsMergeTypeComboBox.SelectedIndex;
            currentPresetSettings.KeywordsMergeMin = (int) keywordsMergeNMinNumericUpDown.Value;
            currentPresetSettings.KeywordsMergeMax = (int) keywordsMergeNMaxNumericUpDown.Value;

            currentPresetSettings.Categories = categoryUseCheckBox.Checked;
            currentPresetSettings.CategoriesType = categoryTypeComboBox.SelectedIndex;
            currentPresetSettings.CategoriesStaticList = categoryStaticCategoriesTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);
            currentPresetSettings.CategoriesDynamicExcludeKeywords = categoryDynamicExcludeKeysCheckBox.Checked;
            currentPresetSettings.CategoriesDynamicMin = (int) categoryDynamicCategoriesMinNumericUpDown.Value;
            currentPresetSettings.CategoriesDynamicMax = (int) categoryDynamicCategoriesMaxNumericUpDown.Value;
            currentPresetSettings.CategoriesDistribute = categoryDistributeComboBox.SelectedIndex;
            currentPresetSettings.CategoriesDistributeContainsID = categoryDistributeIfContainsComboBox.SelectedIndex;
            //Pages
            currentPresetSettings.PagesDoorwayType = pagesDoorwayTypeComboBox.SelectedIndex;

            currentPresetSettings.PagesStaticExtension = pagesStaticBasicExtTextBox.Text;
            currentPresetSettings.PagesStaticSeparator = pagesStaticBasicSeparatorTextBox.Text;
            currentPresetSettings.PagesStaticNamesTypes = pagesStaticNamesTypeComboBox.SelectedIndex;
            currentPresetSettings.PagesStaticPageNames = pagesStaticNamesPageTextBox.Text;
            currentPresetSettings.PagesStaticCategoriesNames = pagesStaticNamesCatTextBox.Text;
            currentPresetSettings.PagesStaticIndexContinues = pagesStaticContinuesIndexCheckBox.Checked;
            currentPresetSettings.PagesStaticIndexContinuesNames = pagesStaticContinuesIndexTextBox.Text;
            currentPresetSettings.PagesStaticCategoriesContinues = pagesStaticContinuesCatCheckBox.Checked;
            currentPresetSettings.PagesStaticCategoriesContinuesNames = pagesStaticContinuesCatTextBox.Text;
            currentPresetSettings.PagesStaticKeysPerPageOnContinues = (int) pagesStaticContinuesKeysOnPageNumericUpDown.Value;

            // Validate static pages names
            if (!ValidateStaticPageNames(currentPresetSettings.PagesStaticPageNames, currentPresetSettings.PagesStaticCategoriesNames))
            {
                throw new Exception("Page names!");
            }

            currentPresetSettings.PagesDynamicPageNames = pagesDynamicNamesPageTextBox.Text;
            currentPresetSettings.PagesDynamicCategoriesNames = pagesDynamicNamesCatTextBox.Text;
            currentPresetSettings.PagesDynamicStaticPageNames = pagesDynamicNamesStaticPageTextBox.Text;
            currentPresetSettings.PagesDynamicIndexContinues = pagesDynamicContinuesIndexCheckBox.Checked;
            currentPresetSettings.PagesDynamicIndexContinuesNames = pagesDynamicContinuesIndexNameTextBox.Text;
            currentPresetSettings.PagesDynamicCategoriesContinues = pagesDynamicContinuesCatCheckBox.Checked;
            currentPresetSettings.PagesDynamicCategoriesContinuesNames1 = pagesDynamicContinuesCatName1TextBox.Text;
            currentPresetSettings.PagesDynamicCategoriesContinuesNames2 = pagesDynamicContinuesCatName2TextBox.Text;
            currentPresetSettings.PagesDynamicKeysPerPageOnContinues = (int) pagesDynamicContinuesKeysOnPageNumericUpDown.Value;

            currentPresetSettings.StaticPages = pagesStaticPagesCheckBox.Checked;
            currentPresetSettings.StaticPagesList = pagesStaticPagesTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);
            currentPresetSettings.StaticPagesIncludeIntoSiteMap = pagesStaticPagesIncludeIntoSiteMapCheckBox.Checked;

            currentPresetSettings.RSS = pagesCreateRSSCheckBox.Checked;
            currentPresetSettings.RSSCount = (int) pagesRSSNumericUpDown.Value;
            currentPresetSettings.RSSFileName = pagesRSSFileName.Text;

            currentPresetSettings.SiteMap = siteMapCheckBox.Checked;
            currentPresetSettings.SiteMapType = siteMapTypeComboBox.SelectedIndex;
            currentPresetSettings.SiteMapHTMLType = siteMapHTMLComboBox.SelectedIndex;
            currentPresetSettings.SiteMapHTMLName = siteMapHTMLNameTextBox.Text;
            currentPresetSettings.SiteMapHTMLLinksMin = (int) siteMapHTMLLinksMinNumericUpDown.Value;
            currentPresetSettings.SiteMapHTMLLinksMax = (int) siteMapHTMLLinksMaxNumericUpDown.Value;

            currentPresetSettings.Robots = robotsCreateCheckBox.Checked;
            currentPresetSettings.RobotsType = robotsComboBox.SelectedIndex;
            currentPresetSettings.RobotsContent = robotsTextBox.Text;
            //Text Generating
            currentPresetSettings.TextGenration = textGeneratingMethodComboBox.SelectedIndex;

            currentPresetSettings.TextGenrationTextLengthMin = (int) textGeneratingTextLengthMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationTextLengthMax = (int) textGeneratingTextLengthMaxNumericUpDown.Value;

            currentPresetSettings.TextGenrationKeywordsMoreThanOneOnPage = textGeneratingKoPMoreThanOneCheckBox.Checked;
            currentPresetSettings.TextGenrationKeywordsOnPageMin = (int) textGeneratingKoPMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationKeywordsOnPageMax = (int) textGeneratingKoPMaxNumericUpDown.Value;

            currentPresetSettings.TextGenrationKeywordsPercentageMin = textGeneratingKeywordsPercentMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationKeywordsPercentageMax = textGeneratingKeywordsPercentMaxNumericUpDown.Value;
            currentPresetSettings.TextGenrationInsertKeywordsType = textGeneratingKeywordsInsertComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationInsertKeywordsСonfusion = (int) textGeneratingKeywordsInsertNumericUpDown.Value;
            currentPresetSettings.TextGenrationPersentageForEachKeyword = textGeneratingKeywordsPfEKCheckBox.Checked;
            currentPresetSettings.TextGenrationInsertOtherKeywords = textGeneratingKeywordsInsertOtherCheckBox.Checked;
            currentPresetSettings.TextGenrationInsertOtherKeywordsPercentage = textGeneratingKeywordsInsertOtherNumericUpDown.Value;

            currentPresetSettings.TextGenrationSelectKeywords = textGeneratingSelectionKeywordsCheckBox.Checked;
            currentPresetSettings.TextGenrationSelectKeywordsTags = textGeneratingSelectionKeywordsTextBox.Text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            currentPresetSettings.TextGenrationSelectPhrases = textGeneratingSelectionPhrasesCheckBox.Checked;
            currentPresetSettings.TextGenrationSelectPhrasesTags = textGeneratingSelectionPhrasesTextBox.Text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            currentPresetSettings.TextGenrationSelectPercentage = textGeneratingSelectionNumericUpDown.Value;

            currentPresetSettings.TextGenrationPunctuationMarks = textGeneratingPunctuationInsertCheckBox.Checked;
            currentPresetSettings.TextGenrationPunctuationMarksInsertMin = (int) textGeneratingPunctuationInsertMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationPunctuationMarksInsertMax = (int) textGeneratingPunctuationInsertMaxNumericUpDown.Value;
            currentPresetSettings.TextGenrationPunctuationMarksList = textGeneratingPunctuationTextBox.Text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            currentPresetSettings.TextGenrationSentencesLengthType = textGeneratingSentencesLengthTypeСomboBox.SelectedIndex;
            currentPresetSettings.TextGenrationSentencesCount = (int) textGeneratingSentencesLengthNumericUpDown.Value;
            currentPresetSettings.TextGenrationSentencesLength = (int) textGeneratingSentencesWordsNumericUpDown.Value;
            currentPresetSettings.TextGenrationSentencesLengthСonfusion = (int) textGeneratingSentencesWordsStepNumericUpDown.Value;
            currentPresetSettings.TextGenrationSentencesMakeBigLetters = textGeneratingSentencesBigLettersCheckBox.Checked;

            currentPresetSettings.TextGenrationParagraphs = textGeneratingParagraphsCheckBox.Checked;
            currentPresetSettings.TextGenrationParagraphsMin = (int) textGeneratingParagraphsMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationParagraphsMax = (int) textGeneratingParagraphsMaxNumericUpDown.Value;

            currentPresetSettings.TextGenrationCGTextAnalyseType = textGeneratingChainTAWordsComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationCGTextAnalyseCutWordsLength = (int) textGeneratingChainTAIfLongerNumericUpDown.Value;
            currentPresetSettings.TextGenrationCGConstructionType = textGeneratingChainConstructionComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationCGConstructionInsertWordsMin = (int) textGeneratingChainConstructionInsertMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationCGConstructionInsertWordsMax = (int) textGeneratingChainConstructionInsertMaxNumericUpDown.Value;
            currentPresetSettings.TextGenrationCGPunctuationMarksConsideration = textGeneratingChainConstructionPunctuationComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationCGConsiderProbability = textGeneratingChainConstructionProbabilityCheckBox.Checked;

            currentPresetSettings.TextGenrationMRAIType = textGenerationMixRandAsIsTypeComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationMRAIPunctuationMarksConsideration = textGenerationMixRandAsIsPunctuationComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationMRAIRadius = (int) textGenerationMixRandAsIsRadiusNumericUpDown.Value;
            currentPresetSettings.TextGenrationMRAIConstructionType = textGenerationMixRandAsIsConstructionComboBox.SelectedIndex;
            currentPresetSettings.TextGenrationMRAIConstructionInsertWordsMin = (int) textGenerationMixRandAsIsConstructionInsertMinNumericUpDown.Value;
            currentPresetSettings.TextGenrationMRAIConstructionInsertWordsMax = (int) textGenerationMixRandAsIsConstructionInsertMaxNumericUpDown.Value;
            //Macroses
            currentPresetSettings.MacrosesMainLinkType = macrosesMainLinkComboBox.SelectedIndex;
            currentPresetSettings.MacrosesMainLink = macrosesMainLinkTextBox.Text;

            currentPresetSettings.MacrosesSite = macrosesSiteTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);
            currentPresetSettings.MacrosesTitle = macrosesTitleTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);

            currentPresetSettings.MacrosesBlockMainMin = (int) macrosesBlockMainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesBlockMainMax = (int) macrosesBlockMainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesBlockPageMin = (int) macrosesBlockPageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesBlockPageMax = (int) macrosesBlockPageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesMenuBlockMainMin = (int) macrosesMenuBlockMainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesMenuBlockMainMax = (int) macrosesMenuBlockMainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesMenuBlockPageMin = (int) macrosesMenuBlockPageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesMenuBlockPageMax = (int) macrosesMenuBlockPageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesUserBlock1MainMin = (int) macrosesUserBlock1MainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock1MainMax = (int) macrosesUserBlock1MainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock1PageMin = (int) macrosesUserBlock1PageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock1PageMax = (int) macrosesUserBlock1PageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesUserBlock2MainMin = (int) macrosesUserBlock2MainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock2MainMax = (int) macrosesUserBlock2MainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock2PageMin = (int) macrosesUserBlock2PageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock2PageMax = (int) macrosesUserBlock2PageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesUserBlock3MainMin = (int) macrosesUserBlock3MainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock3MainMax = (int) macrosesUserBlock3MainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock3PageMin = (int) macrosesUserBlock3PageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock3PageMax = (int) macrosesUserBlock3PageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesUserBlock4MainMin = (int) macrosesUserBlock4MainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock4MainMax = (int) macrosesUserBlock4MainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock4PageMin = (int) macrosesUserBlock4PageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock4PageMax = (int) macrosesUserBlock4PageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesUserBlock5MainMin = (int) macrosesUserBlock5MainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock5MainMax = (int) macrosesUserBlock5MainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock5PageMin = (int) macrosesUserBlock5PageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock5PageMax = (int) macrosesUserBlock5PageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesUserBlock6MainMin = (int) macrosesUserBlock6MainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock6MainMax = (int) macrosesUserBlock6MainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock6PageMin = (int) macrosesUserBlock6PageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesUserBlock6PageMax = (int) macrosesUserBlock6PageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesCategoryMenuBlockMainMin = (int) macrosesCatMenuBlockMainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesCategoryMenuBlockMainMax = (int) macrosesCatMenuBlockMainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesCategoryMenuBlockPageMin = (int) macrosesCatMenuBlockPageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesCategoryMenuBlockPageMax = (int) macrosesCatMenuBlockPageMaxNumericUpDown.Value;

            currentPresetSettings.MacrosesNetBlockMainMin = (int) macrosesNetBlockMainMinNumericUpDown.Value;
            currentPresetSettings.MacrosesNetBlockMainMax = (int) macrosesNetBlockMainMaxNumericUpDown.Value;
            currentPresetSettings.MacrosesNetBlockPageMin = (int) macrosesNetBlockPageMinNumericUpDown.Value;
            currentPresetSettings.MacrosesNetBlockPageMax = (int) macrosesNetBlockPageMaxNumericUpDown.Value;
            //Entrance
            currentPresetSettings.EntranceType = entranceGeneralTypeComboBox.SelectedIndex;
            currentPresetSettings.EntranceInsertType = entranceGeneralInsertTypeComboBox.SelectedIndex;
            currentPresetSettings.EntranceAcceptorAdressType = entranceGeneralAcceptorAdressComboBox.SelectedIndex;
            currentPresetSettings.EntranceAcceptorAdress = entranceGeneralAcceptorAdressTextBox.Text;
            currentPresetSettings.EntranceJSEncrypt = entranceGeneralJSFilesEncriptCheckBox.Checked;

            currentPresetSettings.EntranceCode = entranceRedirectFrameTextBox.Text;
            currentPresetSettings.EntranceCounter = entranceCounterTextBox.Text;
            //Links & Spam
            currentPresetSettings.LinksRelativeURLs = linksInternalRelativeURLsCheckBox.Checked;
            currentPresetSettings.LinksInternal = linksInternalCreateСheckBox.Checked;
            currentPresetSettings.LinksInternalType = linksInternalAnchorsСomboBox.SelectedIndex;
            currentPresetSettings.LinksInternalMinLength = (int) linksInternalLinksLengthMinNumericUpDown.Value;
            currentPresetSettings.LinksInternalMaxLength = (int) linksInternalLinksLengthMaxNumericUpDown.Value;

            currentPresetSettings.LinksExternal = linksExternalUseCheckBox.Checked;
            currentPresetSettings.LinksExternalList = linksExternalTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);

            currentPresetSettings.LinksExternalInText = linksExternalInTextCheckBox.Checked;
            currentPresetSettings.LinksExternalInTextIndexPageMinimum = (int) linksExternalInTextIndexMinNumericUpDown.Value;
            currentPresetSettings.LinksExternalInTextIndexPageMaximum = (int) linksExternalInTextIndexMaxNumericUpDown.Value;
            currentPresetSettings.LinksExternalInTextRegularPageMinimum = (int) linksExternalInTextPageMinNumericUpDown.Value;
            currentPresetSettings.LinksExternalInTextRegularPageMaximum = (int) linksExternalInTextPageMaxNumericUpDown.Value;

            currentPresetSettings.Spam = spamCreateCheckBox.Checked;
            currentPresetSettings.SpamUrlTypeList = spamTypesTextBox.Text.Replace("\r\n", "¥").Split(new char[] {'¥'}, StringSplitOptions.RemoveEmptyEntries);
            currentPresetSettings.SpamSaveToFile = spamSaveCheckBox.Checked;
            currentPresetSettings.SpamSaveToFileType = spamSaveTypeComboBox.SelectedIndex;
            currentPresetSettings.SpamSaveToFilePath = spamSavePathTextBox.Text;

            // Tags
            if (tagsDataGridView.Rows.Count != 0)
            {
                currentPresetSettings.TagSettings = new TagSettings[tagsDataGridView.Rows.Count];

                for (int i = 0; i < tagsDataGridView.Rows.Count; i++)
                {
                    currentPresetSettings.TagSettings[i] = new TagSettings();
                    if (tagsDataGridView.Rows[i].Cells[0].Value == null)
                    {
                        currentPresetSettings.TagSettings[i].File = string.Empty;
                    }
                    else
                    {
                        currentPresetSettings.TagSettings[i].File = tagsDataGridView.Rows[i].Cells[0].Value.ToString();
                    }

                    if (tagsDataGridView.Rows[i].Cells[1].Value == null)
                    {
                        currentPresetSettings.TagSettings[i].EncodingType = 0;
                    }
                    else
                    {
                        currentPresetSettings.TagSettings[i].EncodingType = int.Parse(tagsDataGridView.Rows[i].Cells[1].Value.ToString());
                    }
                }
            }
            else
            {
                currentPresetSettings.TagSettings = new TagSettings[0];
            }

            //FTP
            currentPresetSettings.FTPUpload = ftpGeneralUploadCheckBox.Checked;

            currentPresetSettings.FTPUploadType = ftpGeneralUploadTypeComboBox.SelectedIndex;
            currentPresetSettings.FTPUploadSaveTo = ftpGeneralUploadSettingsUploadSaveToTextBox.Text;

            currentPresetSettings.FTPDelete = ftpGeneralDeleteAfterUploadСheckBox.Checked;
            currentPresetSettings.FTPUploadArchive = ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex;
            currentPresetSettings.FTPUploadArchiveName = ftpGeneralUploadSettingsUploadArchiveTextBox.Text;
            currentPresetSettings.FTPUploadInBackground = ftpGeneralUploadSettingsUploadInBackGroundCheckBox.Checked;
            currentPresetSettings.FTPThreads = (int) ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Value;

            if (ftpGeneralSettingsDataGridView.Rows.Count > 1)
            {
                currentPresetSettings.FTPSettings = new FTPSettings[ftpGeneralSettingsDataGridView.Rows.Count - 1];

                for (int i = 0; i < ftpGeneralSettingsDataGridView.Rows.Count - 1; i++)
                {
                    currentPresetSettings.FTPSettings[i] = new FTPSettings();
                    if (ftpGeneralSettingsDataGridView.Rows[i].Cells[0].Value == null)
                    {
                        currentPresetSettings.FTPSettings[i].Host = string.Empty;
                    }
                    else
                    {
                        currentPresetSettings.FTPSettings[i].Host = ftpGeneralSettingsDataGridView.Rows[i].Cells[0].Value.ToString();
                    }
                    if (ftpGeneralSettingsDataGridView.Rows[i].Cells[1].Value == null)
                    {
                        currentPresetSettings.FTPSettings[i].Login = string.Empty;
                    }
                    else
                    {
                        currentPresetSettings.FTPSettings[i].Login = ftpGeneralSettingsDataGridView.Rows[i].Cells[1].Value.ToString();
                    }
                    if (ftpGeneralSettingsDataGridView.Rows[i].Cells[2].Value == null)
                    {
                        currentPresetSettings.FTPSettings[i].Password = string.Empty;
                    }
                    else
                    {
                        currentPresetSettings.FTPSettings[i].Password = ftpGeneralSettingsDataGridView.Rows[i].Cells[2].Value.ToString();
                    }
                    if (ftpGeneralSettingsDataGridView.Rows[i].Cells[3].Value == null)
                    {
                        currentPresetSettings.FTPSettings[i].Folder = string.Empty;
                    }
                    else
                    {
                        currentPresetSettings.FTPSettings[i].Folder = ftpGeneralSettingsDataGridView.Rows[i].Cells[3].Value.ToString();
                    }
                }
            }
            else
            {
                currentPresetSettings.FTPSettings = new FTPSettings[0];
            }

            // XRumer
            currentPresetSettings.XRumerUse = XrumerUseCheckBox.Checked;
            currentPresetSettings.XRumerDirectory = XrumerDirectoryTextBox.Text;
            currentPresetSettings.XRumerText = XrumerTextTextBox.Text;
            currentPresetSettings.XRumerTemplate = XrumerTemplateTextBox.Text;

            return currentPresetSettings;
        }

        private bool ValidateStaticPageNames(string PageNameTemplate, string CategoryNameTemplate)
        {
            bool result = true;

            if (PageNameTemplate != "[Name]" && PageNameTemplate == CategoryNameTemplate)
            {
                result = false;
            }

            List<string> allowed = new List<string>() { "[Name]", "[N]", "[RN]", "[RENLETTER]", "[RBENLETTER]", "[RRULETTER]", "[RBRULETTER]" };

            // To tokens detected
            if (!PageNameTemplate.ContainsAny(allowed))
            {
                result = false;
            }

            if (!CategoryNameTemplate.ContainsAny(allowed))
            {
                result = false;
            }

            return result;
        }

        public void SetSettings(PresetSettings Settings)
        {
            try
            {
                // General
                generalCreateDoorwaysNumericUpDown.Value = Settings.GeneralCreateDoorways;
                generalCreateThreadsNumericUpDown.Value = Settings.GeneralThreads;
                generalImagesComboBox.SelectedIndex = Settings.GeneralImageType;
                generalImageWidthMinNumericUpDown.Value = Settings.GeneralImageSizeMinWidth;
                generalImageWidthMaxNumericUpDown.Value = Settings.GeneralImageSizeMaxWidth;
                generalImageHeigthMinNumericUpDown.Value = Settings.GeneralImageSizeMinHeight;
                generalImageHeigthMaxNumericUpDown.Value = Settings.GeneralImageSizeMaxHeight;
                generalImagesNewNumericUpDown.Value = Settings.GeneralGenerateImagesCount;

                generalImagesNamingComboBox.SelectedIndex = Settings.GeneralImageNamingType;
                generalImagesNamingTextBox.Text = Settings.GeneralImageNamingFile;

                generalArchiveComboBox.SelectedIndex = Settings.GeneralArchive;
                generalArchiveTextBox.Text = Settings.GeneralArchiveName;

                generalSaveTextBox.Text = Settings.GeneralSaveTo;
                generalCreateSubFolderCheckBox.Checked = Settings.GeneralCreateSubFolders;
                generalSubfoldersComboBox.SelectedIndex = Settings.GeneralSubFoldersType;
                generalDoorwaysLinksTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.GeneralDoorwayUrls.Length; i++)
                {
                    generalDoorwaysLinksTextBox.Text += Settings.GeneralDoorwayUrls[i] + "\r\n";
                }

                generalDatesStartDateTimePicker.Value = Settings.GeneralFileDateStart;
                generalDatesEndDateTimePicker.Value = Settings.GeneralFileDateEnd;

                // FileMacroses
                fileMacrossDataGridView.Rows.Clear();
                if (Settings.FileMacroses.Length != 0)
                {
                    fileMacrossDataGridView.Rows.Add(Settings.FileMacroses.Length);
                    for (int i = 0; i < Settings.FileMacroses.Length; i++)
                    {
                        fileMacrossDataGridView.Rows[i].Cells[0].Value = Settings.FileMacroses[i].Macross;
                        fileMacrossDataGridView.Rows[i].Cells[1].Value = Settings.FileMacroses[i].File;
                        fileMacrossDataGridView.Rows[i].Cells[2].Value = Settings.FileMacroses[i].EncodingType.ToString();
                        fileMacrossDataGridView.Rows[i].Cells[3].Value = Settings.FileMacroses[i].Type.ToString();
                    }
                }

                // Keywords/Categories
                try
                {
                    keywordsComboBox.SelectedIndex = SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].GetKeywordIndex(Settings.KeywordsID);
                }
                catch (Exception) { }

                keywordsUseComboBox.SelectedIndex = Settings.KeywordsSelectType;
                keywordsUseMinNumericUpDown.Value = Settings.KeywordsSelectMin;
                keywordsUseMaxNumericUpDown.Value = Settings.KeywordsSelectMax;
                keywordsReorderKeywordsCheckBox.Checked = Settings.KeywordsReorder;
                keywordsReorderWordsCheckBox.Checked = Settings.KeywordsWordsReorder;
                keywordsReorderWordsCountNumericUpDown.Value = Settings.KeywordsWordsReorderPercentage;
                keywordsSynonymsCheckBox.Checked = Settings.KeywordsSynonyms;
                try
                {
                    keywordsSynonymsComboBox.SelectedIndex = Settings.KeywordsSynonymsID;
                }
                catch (Exception) { }

                keywordsSynonymsNumericUpDown.Value = Settings.KeywordsSynonymsPercentage;
                keywordsMergeCheckBox.Checked = Settings.KeywordsMerge;
                try
                {
                    keywordsMergeComboBox.SelectedIndex = Settings.KeywordsMergeID;
                }
                catch (Exception) { }

                keywordsMergeTypeComboBox.SelectedIndex = Settings.KeywordsMergeType;
                keywordsMergeNMinNumericUpDown.Value = Settings.KeywordsMergeMin;
                keywordsMergeNMaxNumericUpDown.Value = Settings.KeywordsMergeMax;

                categoryUseCheckBox.Checked = Settings.Categories;
                categoryTypeComboBox.SelectedIndex = Settings.CategoriesType;
                categoryStaticCategoriesTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.CategoriesStaticList.Length; i++)
                {
                    categoryStaticCategoriesTextBox.Text += Settings.CategoriesStaticList[i] + "\r\n";
                }

                categoryDynamicExcludeKeysCheckBox.Checked = Settings.CategoriesDynamicExcludeKeywords;
                categoryDynamicCategoriesMinNumericUpDown.Value = Settings.CategoriesDynamicMin;
                categoryDynamicCategoriesMaxNumericUpDown.Value = Settings.CategoriesDynamicMax;
                categoryDistributeComboBox.SelectedIndex = Settings.CategoriesDistribute;
                categoryDistributeIfContainsComboBox.SelectedIndex = Settings.CategoriesDistributeContainsID;

                // Pages
                pagesDoorwayTypeComboBox.SelectedIndex = Settings.PagesDoorwayType;

                pagesStaticBasicExtTextBox.Text = Settings.PagesStaticExtension;
                pagesStaticBasicSeparatorTextBox.Text = Settings.PagesStaticSeparator;
                pagesStaticNamesTypeComboBox.SelectedIndex = Settings.PagesStaticNamesTypes;
                pagesStaticNamesPageTextBox.Text = Settings.PagesStaticPageNames;
                pagesStaticNamesCatTextBox.Text = Settings.PagesStaticCategoriesNames;
                pagesStaticContinuesIndexCheckBox.Checked = Settings.PagesStaticIndexContinues;
                pagesStaticContinuesIndexTextBox.Text = Settings.PagesStaticIndexContinuesNames;
                pagesStaticContinuesCatCheckBox.Checked = Settings.PagesStaticCategoriesContinues;
                pagesStaticContinuesCatTextBox.Text = Settings.PagesStaticCategoriesContinuesNames;
                pagesStaticContinuesKeysOnPageNumericUpDown.Value = Settings.PagesStaticKeysPerPageOnContinues;

                pagesDynamicNamesPageTextBox.Text = Settings.PagesDynamicPageNames;
                pagesDynamicNamesCatTextBox.Text = Settings.PagesDynamicCategoriesNames;
                pagesDynamicNamesStaticPageTextBox.Text = Settings.PagesDynamicStaticPageNames;
                pagesDynamicContinuesIndexCheckBox.Checked = Settings.PagesDynamicIndexContinues;
                pagesDynamicContinuesIndexNameTextBox.Text = Settings.PagesDynamicIndexContinuesNames;
                pagesDynamicContinuesCatCheckBox.Checked = Settings.PagesDynamicCategoriesContinues;
                pagesDynamicContinuesCatName1TextBox.Text = Settings.PagesDynamicCategoriesContinuesNames1;
                pagesDynamicContinuesCatName2TextBox.Text = Settings.PagesDynamicCategoriesContinuesNames2;
                pagesDynamicContinuesKeysOnPageNumericUpDown.Value = Settings.PagesDynamicKeysPerPageOnContinues;

                pagesStaticPagesCheckBox.Checked = Settings.StaticPages;
                pagesStaticPagesTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.StaticPagesList.Length; i++)
                {
                    pagesStaticPagesTextBox.Text += Settings.StaticPagesList[i] + "\r\n";
                }

                pagesStaticPagesIncludeIntoSiteMapCheckBox.Checked = Settings.StaticPagesIncludeIntoSiteMap;

                pagesCreateRSSCheckBox.Checked = Settings.RSS;
                pagesRSSNumericUpDown.Value = Settings.RSSCount;
                pagesRSSFileName.Text = Settings.RSSFileName;

                siteMapCheckBox.Checked = Settings.SiteMap;
                siteMapTypeComboBox.SelectedIndex = Settings.SiteMapType;
                siteMapHTMLComboBox.SelectedIndex = Settings.SiteMapHTMLType;
                siteMapHTMLNameTextBox.Text = Settings.SiteMapHTMLName;
                siteMapHTMLLinksMinNumericUpDown.Value = Settings.SiteMapHTMLLinksMin;
                siteMapHTMLLinksMaxNumericUpDown.Value = Settings.SiteMapHTMLLinksMax;
                
                robotsCreateCheckBox.Checked = Settings.Robots;
                robotsComboBox.SelectedIndex = Settings.RobotsType;
                robotsTextBox.Text = Settings.RobotsContent;

                // Invoking MakeRobots to regenerate
                if (robotsCreateCheckBox.Checked && robotsComboBox.SelectedIndex == 1 && generalDoorwaysLinksTextBox.Text != string.Empty && (siteMapTypeComboBox.SelectedIndex == 0 || siteMapTypeComboBox.SelectedIndex == 2))
                {
                    if (!robotsTextBox.Text.EndsWith("Sitemap: [SITEMAP]"))
                    {
                        robotsTextBox.Text += "\r\nSitemap: [SITEMAP]";
                    }
                }

                // Text Generating
                textGeneratingMethodComboBox.SelectedIndex = Settings.TextGenration;

                textGeneratingTextLengthMinNumericUpDown.Value = Settings.TextGenrationTextLengthMin;
                textGeneratingTextLengthMaxNumericUpDown.Value = Settings.TextGenrationTextLengthMax;

                textGeneratingKoPMoreThanOneCheckBox.Checked = Settings.TextGenrationKeywordsMoreThanOneOnPage;
                textGeneratingKoPMinNumericUpDown.Value = Settings.TextGenrationKeywordsOnPageMin;
                textGeneratingKoPMaxNumericUpDown.Value = Settings.TextGenrationKeywordsOnPageMax;

                textGeneratingKeywordsPercentMinNumericUpDown.Value = Settings.TextGenrationKeywordsPercentageMin;
                textGeneratingKeywordsPercentMaxNumericUpDown.Value = Settings.TextGenrationKeywordsPercentageMax;
                textGeneratingKeywordsInsertComboBox.SelectedIndex = Settings.TextGenrationInsertKeywordsType;
                textGeneratingKeywordsInsertNumericUpDown.Value = Settings.TextGenrationInsertKeywordsСonfusion;
                textGeneratingKeywordsPfEKCheckBox.Checked = Settings.TextGenrationPersentageForEachKeyword;
                textGeneratingKeywordsInsertOtherCheckBox.Checked = Settings.TextGenrationInsertOtherKeywords;
                textGeneratingKeywordsInsertOtherNumericUpDown.Value = Settings.TextGenrationInsertOtherKeywordsPercentage;

                textGeneratingSelectionKeywordsCheckBox.Checked = Settings.TextGenrationSelectKeywords;
                textGeneratingSelectionKeywordsTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.TextGenrationSelectKeywordsTags.Length; i++)
                {
                    textGeneratingSelectionKeywordsTextBox.Text += Settings.TextGenrationSelectKeywordsTags[i] + " ";
                }

                textGeneratingSelectionPhrasesCheckBox.Checked = Settings.TextGenrationSelectPhrases;
                textGeneratingSelectionPhrasesTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.TextGenrationSelectPhrasesTags.Length; i++)
                {
                    textGeneratingSelectionPhrasesTextBox.Text += Settings.TextGenrationSelectPhrasesTags[i] + " ";
                }

                textGeneratingSelectionNumericUpDown.Value = Settings.TextGenrationSelectPercentage;


                textGeneratingPunctuationInsertCheckBox.Checked = Settings.TextGenrationPunctuationMarks;
                textGeneratingPunctuationInsertMinNumericUpDown.Value = Settings.TextGenrationPunctuationMarksInsertMin;
                textGeneratingPunctuationInsertMaxNumericUpDown.Value = Settings.TextGenrationPunctuationMarksInsertMax;
                textGeneratingPunctuationTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.TextGenrationPunctuationMarksList.Length; i++)
                {
                    textGeneratingPunctuationTextBox.Text += Settings.TextGenrationPunctuationMarksList[i] + " ";
                }

                textGeneratingSentencesLengthTypeСomboBox.SelectedIndex = Settings.TextGenrationSentencesLengthType;
                textGeneratingSentencesLengthNumericUpDown.Value = Settings.TextGenrationSentencesCount;
                textGeneratingSentencesWordsNumericUpDown.Value = Settings.TextGenrationSentencesLength;
                textGeneratingSentencesWordsStepNumericUpDown.Value = Settings.TextGenrationSentencesLengthСonfusion;
                textGeneratingSentencesBigLettersCheckBox.Checked = Settings.TextGenrationSentencesMakeBigLetters;

                textGeneratingParagraphsCheckBox.Checked = Settings.TextGenrationParagraphs;
                textGeneratingParagraphsMinNumericUpDown.Value = Settings.TextGenrationParagraphsMin;
                textGeneratingParagraphsMaxNumericUpDown.Value = Settings.TextGenrationParagraphsMax;

                textGeneratingChainTAWordsComboBox.SelectedIndex = Settings.TextGenrationCGTextAnalyseType;
                textGeneratingChainTAIfLongerNumericUpDown.Value = Settings.TextGenrationCGTextAnalyseCutWordsLength;
                textGeneratingChainConstructionComboBox.SelectedIndex = Settings.TextGenrationCGConstructionType;
                textGeneratingChainConstructionInsertMinNumericUpDown.Value = Settings.TextGenrationCGConstructionInsertWordsMin;
                textGeneratingChainConstructionInsertMaxNumericUpDown.Value = Settings.TextGenrationCGConstructionInsertWordsMax;
                textGeneratingChainConstructionPunctuationComboBox.SelectedIndex = Settings.TextGenrationCGPunctuationMarksConsideration;
                textGeneratingChainConstructionProbabilityCheckBox.Checked = Settings.TextGenrationCGConsiderProbability;

                textGenerationMixRandAsIsTypeComboBox.SelectedIndex = Settings.TextGenrationMRAIType;
                textGenerationMixRandAsIsPunctuationComboBox.SelectedIndex = Settings.TextGenrationMRAIPunctuationMarksConsideration;
                textGenerationMixRandAsIsRadiusNumericUpDown.Value = Settings.TextGenrationMRAIRadius;
                textGenerationMixRandAsIsConstructionComboBox.SelectedIndex = Settings.TextGenrationMRAIConstructionType;
                textGenerationMixRandAsIsConstructionInsertMinNumericUpDown.Value = Settings.TextGenrationMRAIConstructionInsertWordsMin;
                textGenerationMixRandAsIsConstructionInsertMaxNumericUpDown.Value = Settings.TextGenrationMRAIConstructionInsertWordsMax;

                // Macroses
                macrosesMainLinkComboBox.SelectedIndex = Settings.MacrosesMainLinkType;
                macrosesMainLinkTextBox.Text = Settings.MacrosesMainLink;

                macrosesSiteTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.MacrosesSite.Length; i++)
                {
                    macrosesSiteTextBox.Text += Settings.MacrosesSite[i];
                    if ((i + 1) < Settings.MacrosesSite.Length)
                    {
                        macrosesSiteTextBox.Text += "\r\n";
                    }
                }

                macrosesTitleTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.MacrosesTitle.Length; i++)
                {
                    macrosesTitleTextBox.Text += Settings.MacrosesTitle[i];
                    if ((i + 1) < Settings.MacrosesTitle.Length)
                    {
                        macrosesTitleTextBox.Text += "\r\n";
                    }
                }

                macrosesBlockMainMinNumericUpDown.Value = Settings.MacrosesBlockMainMin;
                macrosesBlockMainMaxNumericUpDown.Value = Settings.MacrosesBlockMainMax;
                macrosesBlockPageMinNumericUpDown.Value = Settings.MacrosesBlockPageMin;
                macrosesBlockPageMaxNumericUpDown.Value = Settings.MacrosesBlockPageMax;

                macrosesMenuBlockMainMinNumericUpDown.Value = Settings.MacrosesMenuBlockMainMin;
                macrosesMenuBlockMainMaxNumericUpDown.Value = Settings.MacrosesMenuBlockMainMax;
                macrosesMenuBlockPageMinNumericUpDown.Value = Settings.MacrosesMenuBlockPageMin;
                macrosesMenuBlockPageMaxNumericUpDown.Value = Settings.MacrosesMenuBlockPageMax;

                macrosesUserBlock1MainMinNumericUpDown.Value = Settings.MacrosesUserBlock1MainMin;
                macrosesUserBlock1MainMaxNumericUpDown.Value = Settings.MacrosesUserBlock1MainMax;
                macrosesUserBlock1PageMinNumericUpDown.Value = Settings.MacrosesUserBlock1PageMin;
                macrosesUserBlock1PageMaxNumericUpDown.Value = Settings.MacrosesUserBlock1PageMax;

                macrosesUserBlock2MainMinNumericUpDown.Value = Settings.MacrosesUserBlock2MainMin;
                macrosesUserBlock2MainMaxNumericUpDown.Value = Settings.MacrosesUserBlock2MainMax;
                macrosesUserBlock2PageMinNumericUpDown.Value = Settings.MacrosesUserBlock2PageMin;
                macrosesUserBlock2PageMaxNumericUpDown.Value = Settings.MacrosesUserBlock2PageMax;

                macrosesUserBlock3MainMinNumericUpDown.Value = Settings.MacrosesUserBlock3MainMin;
                macrosesUserBlock3MainMaxNumericUpDown.Value = Settings.MacrosesUserBlock3MainMax;
                macrosesUserBlock3PageMinNumericUpDown.Value = Settings.MacrosesUserBlock3PageMin;
                macrosesUserBlock3PageMaxNumericUpDown.Value = Settings.MacrosesUserBlock3PageMax;

                macrosesUserBlock4MainMinNumericUpDown.Value = Settings.MacrosesUserBlock4MainMin;
                macrosesUserBlock4MainMaxNumericUpDown.Value = Settings.MacrosesUserBlock4MainMax;
                macrosesUserBlock4PageMinNumericUpDown.Value = Settings.MacrosesUserBlock4PageMin;
                macrosesUserBlock4PageMaxNumericUpDown.Value = Settings.MacrosesUserBlock4PageMax;

                macrosesUserBlock5MainMinNumericUpDown.Value = Settings.MacrosesUserBlock5MainMin;
                macrosesUserBlock5MainMaxNumericUpDown.Value = Settings.MacrosesUserBlock5MainMax;
                macrosesUserBlock5PageMinNumericUpDown.Value = Settings.MacrosesUserBlock5PageMin;
                macrosesUserBlock5PageMaxNumericUpDown.Value = Settings.MacrosesUserBlock5PageMax;

                macrosesUserBlock6MainMinNumericUpDown.Value = Settings.MacrosesUserBlock6MainMin;
                macrosesUserBlock6MainMaxNumericUpDown.Value = Settings.MacrosesUserBlock6MainMax;
                macrosesUserBlock6PageMinNumericUpDown.Value = Settings.MacrosesUserBlock6PageMin;
                macrosesUserBlock6PageMaxNumericUpDown.Value = Settings.MacrosesUserBlock6PageMax;

                macrosesCatMenuBlockMainMinNumericUpDown.Value = Settings.MacrosesCategoryMenuBlockMainMin;
                macrosesCatMenuBlockMainMaxNumericUpDown.Value = Settings.MacrosesCategoryMenuBlockMainMax;
                macrosesCatMenuBlockPageMinNumericUpDown.Value = Settings.MacrosesCategoryMenuBlockPageMin;
                macrosesCatMenuBlockPageMaxNumericUpDown.Value = Settings.MacrosesCategoryMenuBlockPageMax;

                macrosesNetBlockMainMinNumericUpDown.Value = Settings.MacrosesNetBlockMainMin;
                macrosesNetBlockMainMaxNumericUpDown.Value = Settings.MacrosesNetBlockMainMax;
                macrosesNetBlockPageMinNumericUpDown.Value = Settings.MacrosesNetBlockPageMin;
                macrosesNetBlockPageMaxNumericUpDown.Value = Settings.MacrosesNetBlockPageMax;
                //Entrance
                entranceGeneralTypeComboBox.SelectedIndex = Settings.EntranceType;
                entranceGeneralInsertTypeComboBox.SelectedIndex = Settings.EntranceInsertType;
                entranceGeneralAcceptorAdressComboBox.SelectedIndex = Settings.EntranceAcceptorAdressType;
                entranceGeneralAcceptorAdressTextBox.Text = Settings.EntranceAcceptorAdress;
                entranceGeneralJSFilesEncriptCheckBox.Checked = Settings.EntranceJSEncrypt;

                entranceRedirectFrameTextBox.Text = Settings.EntranceCode;
                entranceCounterTextBox.Text = Settings.EntranceCounter;
                //Links & Spam
                linksInternalRelativeURLsCheckBox.Checked = Settings.LinksRelativeURLs;
                linksInternalCreateСheckBox.Checked = Settings.LinksInternal;
                linksInternalAnchorsСomboBox.SelectedIndex = Settings.LinksInternalType;
                linksInternalLinksLengthMinNumericUpDown.Value = Settings.LinksInternalMinLength;
                linksInternalLinksLengthMaxNumericUpDown.Value = Settings.LinksInternalMaxLength;

                linksExternalUseCheckBox.Checked = Settings.LinksExternal;
                linksExternalTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.LinksExternalList.Length; i++)
                {
                    linksExternalTextBox.Text += Settings.LinksExternalList[i] + "\r\n";
                }

                linksExternalInTextCheckBox.Checked = Settings.LinksExternalInText;
                linksExternalInTextIndexMinNumericUpDown.Value = Settings.LinksExternalInTextIndexPageMinimum;
                linksExternalInTextIndexMaxNumericUpDown.Value = Settings.LinksExternalInTextIndexPageMaximum;
                linksExternalInTextPageMinNumericUpDown.Value = Settings.LinksExternalInTextRegularPageMinimum;
                linksExternalInTextPageMaxNumericUpDown.Value = Settings.LinksExternalInTextRegularPageMaximum;

                spamCreateCheckBox.Checked = Settings.Spam;
                spamTypesTextBox.Text = string.Empty;
                for (int i = 0; i < Settings.SpamUrlTypeList.Length; i++)
                {
                    spamTypesTextBox.Text += Settings.SpamUrlTypeList[i] + "\r\n";
                }
                spamSaveCheckBox.Checked = Settings.SpamSaveToFile;
                spamSaveTypeComboBox.SelectedIndex = Settings.SpamSaveToFileType;
                spamSavePathTextBox.Text = Settings.SpamSaveToFilePath;

                // Tags
                for (int i = 0; i < Settings.TagSettings.Length; i++)
                {
                    tagsDataGridView.Rows.Add();
                    tagsDataGridView.Rows[i].Cells[0].Value = Settings.TagSettings[i].File;
                    tagsDataGridView.Rows[i].Cells[1].Value = Settings.TagSettings[i].EncodingType.ToString();
                }

                //FTP
                ftpGeneralUploadCheckBox.Checked = Settings.FTPUpload;

                ftpGeneralUploadTypeComboBox.SelectedIndex = Settings.FTPUploadType;
                ftpGeneralUploadSettingsUploadSaveToTextBox.Text = Settings.FTPUploadSaveTo;

                ftpGeneralDeleteAfterUploadСheckBox.Checked = Settings.FTPDelete;
                ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex = Settings.FTPUploadArchive;
                ftpGeneralUploadSettingsUploadArchiveTextBox.Text = Settings.FTPUploadArchiveName;
                ftpGeneralUploadSettingsUploadInBackGroundCheckBox.Checked = Settings.FTPUploadInBackground;
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Value = Settings.FTPThreads;

                ftpGeneralSettingsDataGridView.Rows.Clear();
                for (int i = 0; i < Settings.FTPSettings.Length; i++)
                {
                    ftpGeneralSettingsDataGridView.Rows.Add();
                    ftpGeneralSettingsDataGridView.Rows[i].Cells[0].Value = Settings.FTPSettings[i].Host;
                    ftpGeneralSettingsDataGridView.Rows[i].Cells[1].Value = Settings.FTPSettings[i].Login;
                    ftpGeneralSettingsDataGridView.Rows[i].Cells[2].Value = Settings.FTPSettings[i].Password;
                    ftpGeneralSettingsDataGridView.Rows[i].Cells[3].Value = Settings.FTPSettings[i].Folder;
                }

                // XRumer
                XrumerUseCheckBox.Checked = Settings.XRumerUse;
                XrumerDirectoryTextBox.Text = Settings.XRumerDirectory;
                XrumerTextTextBox.Text = Settings.XRumerText;
                XrumerTemplateTextBox.Text = Settings.XRumerTemplate;
            }
            catch (Exception)
            {
                MessageBox.Show(View.UILanguageResources.GetString("S0000277"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void generalWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (generalWSComboBox.SelectedIndex == -1)
            {
                return;
            }
            //Заполнение полей
            //Templates
            generalTemplateComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates.Count; i++)
            {
                generalTemplateComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[i].Name);
            }
            //Presets
            generalPresetComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Presets.Count; i++)
            {
                generalPresetComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Presets[i].Name);
            }
            //Texts
            generalTextComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Texts.Count; i++)
            {
                generalTextComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Texts[i].Name);
            }
            //Keywords & Synonyms & Merge & Categories
            keywordsComboBox.Items.Clear();
            keywordsSynonymsComboBox.Items.Clear();
            keywordsMergeComboBox.Items.Clear();
            categoryDistributeIfContainsComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Keywords.Count; i++)
            {
                keywordsComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Keywords[i].Name);
                keywordsSynonymsComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Keywords[i].Name);
                keywordsMergeComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Keywords[i].Name);
                categoryDistributeIfContainsComboBox.Items.Add(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Keywords[i].Name);
            }
        }

        private void generalPresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (generalWSComboBox.SelectedIndex == -1)
            {
                return;
            }
            //Заполнение полей
            SetSettings(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Presets[generalPresetComboBox.SelectedIndex].Settings);
            //Шаблон
            try
            {
                generalTemplateComboBox.SelectedIndex = SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].GetTemplateIndex(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Presets[generalPresetComboBox.SelectedIndex].TemplateID);
            }
            catch (Exception)
            {
            }
            //Текста
            try
            {
                generalTextComboBox.SelectedIndex = SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].GetTextIndex(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Presets[generalPresetComboBox.SelectedIndex].TextID);
            }
            catch (Exception)
            {
            }
        }

        private void generalImagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (generalImagesComboBox.SelectedIndex == -1)
            {
                return;
            }

            switch (generalImagesComboBox.SelectedIndex)
            {
                case 0:
                case 1:
                    {
                        generalImageSizeGroupBox.Enabled = false;
                        break;
                    }

                case 5:
                    {
                        generalImageSizeGroupBox.Enabled = false;
                        break;
                    }

                default:
                    {
                        generalImageSizeGroupBox.Enabled = true;
                        break;
                    }
            }

            if (generalImagesComboBox.SelectedIndex > 0 && generalImagesComboBox.SelectedIndex < 5)
            {
                generalImagesNamingGroupBox.Enabled = true;
            }
            else
            {
                generalImagesNamingGroupBox.Enabled = false;
            }
        }

        private void generalCreateSubFolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            generalSubfoldersComboBox.Enabled = generalCreateSubFolderCheckBox.Checked;
        }

        private void generalSaveButton_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = string.Empty;
            saveToFolderBrowserDialog.ShowDialog();
            if (saveToFolderBrowserDialog.SelectedPath == string.Empty)
            {
                return;
            }

            generalSaveTextBox.Text = saveToFolderBrowserDialog.SelectedPath;
        }

        private void fileMacrossFileSelectButton_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (openTextFileDialog.FileName == string.Empty)
            {
                return;
            }

            fileMacrossPathTextBox.Text = openTextFileDialog.FileName;
        }

        private void fileMacrossAddFileMacrossButton_Click(object sender, EventArgs e)
        {
            if (fileMacrossPathTextBox.Text != string.Empty && fileMacrossTextBox.Text != string.Empty)
            {
                // Добавление
                fileMacrossDataGridView.Rows.Add();

                fileMacrossDataGridView.Rows[fileMacrossDataGridView.Rows.Count - 1].Cells[0].Value = fileMacrossTextBox.Text;
                fileMacrossDataGridView.Rows[fileMacrossDataGridView.Rows.Count - 1].Cells[1].Value = fileMacrossPathTextBox.Text;
                fileMacrossDataGridView.Rows[fileMacrossDataGridView.Rows.Count - 1].Cells[2].Value = fileMacrossEncodingComboBox.SelectedIndex.ToString();
                fileMacrossDataGridView.Rows[fileMacrossDataGridView.Rows.Count - 1].Cells[3].Value = fileMacrossTypeComboBox.SelectedIndex.ToString();

                // Очистка
                fileMacrossPathTextBox.Text = string.Empty;
                fileMacrossTextBox.Text = string.Empty;
                fileMacrossEncodingComboBox.SelectedIndex = 0;
                fileMacrossTypeComboBox.SelectedIndex = 0;
            }
        }

        private void fileMacrossDeleteFileMacrossButton_Click(object sender, EventArgs e)
        {
            if (fileMacrossDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRow = fileMacrossDataGridView.SelectedRows;
                fileMacrossDataGridView.Rows.Remove(selectedRow[0]);
            }
        }

        private void keywordsUseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (keywordsUseComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (keywordsUseComboBox.SelectedIndex == 0)
            {
                keywordsUseMinNumericUpDown.Enabled = false;
                keywordsUseMaxNumericUpDown.Enabled = false;
            }
            else
            {
                keywordsUseMinNumericUpDown.Enabled = true;
                keywordsUseMaxNumericUpDown.Enabled = true;
            }
        }

        private void keywordsReorderWordsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (keywordsReorderWordsCheckBox.Checked)
            {
                keywordsReorderWordsCountNumericUpDown.Enabled = true;
            }
            else
            {
                keywordsReorderWordsCountNumericUpDown.Enabled = false;
            }
        }

        private void keywordsSynonymsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (keywordsSynonymsCheckBox.Checked)
            {
                keywordsSynonymsNumericUpDown.Enabled = true;
                keywordsSynonymsComboBox.Enabled = true;
            }
            else
            {
                keywordsSynonymsNumericUpDown.Enabled = false;
                keywordsSynonymsComboBox.Enabled = false;
            }
        }

        private void keywordsMergeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (keywordsMergeCheckBox.Checked)
            {
                keywordsMergeComboBox.Enabled = true;
                keywordsMergeTypeComboBox.Enabled = true;
                keywordsMergeNMinNumericUpDown.Enabled = true;
                keywordsMergeNMaxNumericUpDown.Enabled = true;
            }
            else
            {
                keywordsMergeComboBox.Enabled = false;
                keywordsMergeTypeComboBox.Enabled = false;
                keywordsMergeNMinNumericUpDown.Enabled = false;
                keywordsMergeNMaxNumericUpDown.Enabled = false;
            }
        }

        private void categoryUseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (categoryUseCheckBox.Checked)
            {
                categoryTypeComboBox.Enabled = true;

                pagesStaticNamesPageTextBox.Text = "[CName]/[Name]";
                pagesStaticNamesCatTextBox.Text = "[Name]";
                pagesStaticContinuesCatTextBox.Text = "[Name]-[N]";

                switch (pagesDoorwayTypeComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            pagesStaticNamesCatTextBox.Enabled = true;
                            pagesStaticContinuesCatCheckBox.Enabled = true;
                            if (pagesStaticContinuesCatCheckBox.Checked)
                            {
                                pagesStaticContinuesCatTextBox.Enabled = true;
                            }
                            else
                            {
                                pagesStaticContinuesCatTextBox.Enabled = false;
                            }
                            categoryDynamicExcludeKeysCheckBox.Enabled = true;

                            pagesDynamicNamesCatTextBox.Enabled = false;
                            pagesDynamicContinuesCatCheckBox.Enabled = false;
                            pagesDynamicContinuesCatName1TextBox.Enabled = false;
                            pagesDynamicContinuesCatName2TextBox.Enabled = false;

                            break;
                        }
                    case 1:
                        {
                            pagesStaticNamesCatTextBox.Enabled = false;
                            pagesStaticContinuesCatCheckBox.Enabled = false;
                            pagesStaticContinuesCatTextBox.Enabled = false;
                            categoryDynamicExcludeKeysCheckBox.Enabled = false;

                            pagesDynamicNamesCatTextBox.Enabled = true;
                            pagesDynamicContinuesCatCheckBox.Enabled = true;
                            pagesDynamicContinuesCatName1TextBox.Enabled = true;
                            pagesDynamicContinuesCatName2TextBox.Enabled = true;

                            break;
                        }
                }
                switch (categoryTypeComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            categoryDynamicGroupBox.Enabled = true;
                            categoryStaticGroupBox.Enabled = false;
                            categoryDistributeGroupBox.Enabled = true;

                            break;
                        }
                    case 1:
                        {
                            categoryDynamicGroupBox.Enabled = false;
                            categoryStaticGroupBox.Enabled = true;
                            categoryDistributeGroupBox.Enabled = false;

                            break;
                        }
                }
            }
            else
            {
                pagesStaticNamesPageTextBox.Text = "[Name]";

                pagesStaticNamesCatTextBox.Enabled = false;
                pagesStaticContinuesCatCheckBox.Enabled = false;
                pagesStaticContinuesCatTextBox.Enabled = false;

                pagesDynamicNamesCatTextBox.Enabled = false;
                pagesDynamicContinuesCatCheckBox.Enabled = false;
                pagesDynamicContinuesCatCheckBox.Enabled = false;
                pagesDynamicContinuesCatName1TextBox.Enabled = false;
                pagesDynamicContinuesCatName2TextBox.Enabled = false;

                categoryTypeComboBox.Enabled = false;
                categoryDynamicGroupBox.Enabled = false;
                categoryDynamicExcludeKeysCheckBox.Enabled = false;
                categoryStaticGroupBox.Enabled = false;
                categoryDistributeGroupBox.Enabled = false;
            }
        }
        private void categoryTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryTypeComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (categoryUseCheckBox.Checked)
            {
                switch (categoryTypeComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            //categoryDynamicGroupBox.Enabled = true;
                            categoryStaticGroupBox.Enabled = false;
                            categoryDistributeGroupBox.Enabled = true;
                            categoryDynamicExcludeKeysCheckBox.Enabled = true;
                            break;
                        }
                    case 1:
                        {
                            //categoryDynamicGroupBox.Enabled = false;
                            categoryStaticGroupBox.Enabled = true;
                            categoryDistributeGroupBox.Enabled = false;
                            categoryDynamicExcludeKeysCheckBox.Enabled = false;
                            break;
                        }
                }
            }
            else
            {
                categoryDynamicGroupBox.Enabled = false;
                categoryStaticGroupBox.Enabled = false;
                categoryDistributeGroupBox.Enabled = false;
                categoryDynamicExcludeKeysCheckBox.Enabled = false;
            }
        }

        private void categoryDistributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryUseCheckBox.Checked)
            {
                if (categoryTypeComboBox.SelectedIndex == 0)
                {
                    if (categoryDistributeComboBox.SelectedIndex == 1)
                    {
                        categoryDistributeIfContainsComboBox.Enabled = true;
                        categoryDynamicGroupBox.Enabled = false;
                    }
                    else
                    {
                        categoryDistributeIfContainsComboBox.Enabled = false;
                        categoryDynamicGroupBox.Enabled = true;
                    }
                }
            }
            else
            {
                categoryDynamicGroupBox.Enabled = false;
                categoryStaticGroupBox.Enabled = false;
                categoryDistributeGroupBox.Enabled = false;
            }
        }

        private void pagesDoorwayTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pagesDoorwayTypeComboBox.SelectedIndex == 0)
            {
                pagesStaticNamesPageTextBox.Enabled = true;
                pagesStaticContinuesCatCheckBox.Enabled = true;
                pagesStaticContinuesCatTextBox.Enabled = true;
                pagesStaticBasicExtTextBox.Enabled = true;
                pagesStaticBasicSeparatorTextBox.Enabled = true;
                pagesStaticNamesTypeComboBox.Enabled = true;
                pagesStaticContinuesIndexCheckBox.Enabled = true;

                if (categoryUseCheckBox.Checked)
                {
                    pagesStaticNamesCatTextBox.Enabled = true;
                    pagesStaticContinuesCatCheckBox.Enabled = true;
                    pagesStaticContinuesCatTextBox.Enabled = true;
                }
                else
                {
                    pagesStaticNamesCatTextBox.Enabled = false;
                    pagesStaticContinuesCatCheckBox.Checked = false;
                    pagesStaticContinuesCatCheckBox.Enabled = false;
                    pagesStaticContinuesCatTextBox.Enabled = false;
                }

                pagesDynamicNamesPageTextBox.Enabled = false;
                pagesDynamicNamesCatTextBox.Enabled = false;
                pagesDynamicNamesStaticPageTextBox.Enabled = false;
                pagesDynamicContinuesIndexCheckBox.Enabled = false;
                pagesDynamicContinuesCatCheckBox.Enabled = false;
                pagesDynamicContinuesCatName1TextBox.Enabled = false;
                pagesDynamicContinuesCatName2TextBox.Enabled = false;
            }
            else if (pagesDoorwayTypeComboBox.SelectedIndex == 1 || pagesDoorwayTypeComboBox.SelectedIndex == 2)
            {
                pagesStaticNamesPageTextBox.Enabled = false;
                pagesStaticNamesCatTextBox.Enabled = false;
                if (pagesStaticPagesCheckBox.Checked)
                {
                    pagesDynamicNamesStaticPageTextBox.Enabled = true;
                }
                else
                {
                    pagesDynamicNamesStaticPageTextBox.Enabled = false;
                }
                pagesStaticContinuesCatCheckBox.Enabled = false;
                pagesStaticContinuesCatTextBox.Enabled = false;
                pagesStaticBasicExtTextBox.Enabled = false;
                pagesStaticBasicSeparatorTextBox.Enabled = false;
                pagesStaticNamesTypeComboBox.Enabled = false;
                pagesStaticContinuesIndexCheckBox.Enabled = false;


                pagesDynamicNamesPageTextBox.Enabled = true;
                pagesDynamicContinuesIndexCheckBox.Enabled = true;

                if (categoryUseCheckBox.Checked)
                {
                    pagesDynamicNamesCatTextBox.Enabled = true;
                    pagesDynamicContinuesCatCheckBox.Enabled = true;
                    pagesDynamicContinuesCatName1TextBox.Enabled = true;
                    pagesDynamicContinuesCatName2TextBox.Enabled = true;
                }
                else
                {
                    pagesDynamicNamesCatTextBox.Enabled = false;
                    pagesDynamicContinuesCatCheckBox.Enabled = false;
                    pagesDynamicContinuesCatName1TextBox.Enabled = false;
                    pagesDynamicContinuesCatName2TextBox.Enabled = false;
                }
            }
        }

        private void pagesStaticContinuesIndexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pagesStaticContinuesIndexCheckBox.Checked)
            {
                pagesStaticContinuesIndexTextBox.Enabled = true;
            }
            else
            {
                pagesStaticContinuesIndexTextBox.Enabled = false;
            }
            if (pagesStaticContinuesIndexCheckBox.Checked || pagesStaticContinuesCatCheckBox.Checked)
            {
                pagesStaticContinuesKeysOnPageNumericUpDown.Enabled = true;
            }
            else
            {
                pagesStaticContinuesKeysOnPageNumericUpDown.Enabled = false;
            }
        }

        private void pagesStaticContinuesCatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pagesStaticContinuesCatCheckBox.Checked)
            {
                pagesStaticContinuesCatTextBox.Enabled = true;
            }
            else
            {
                pagesStaticContinuesCatTextBox.Enabled = false;
            }
            if (pagesStaticContinuesIndexCheckBox.Checked || pagesStaticContinuesCatCheckBox.Checked)
            {
                pagesStaticContinuesKeysOnPageNumericUpDown.Enabled = true;
            }
            else
            {
                pagesStaticContinuesKeysOnPageNumericUpDown.Enabled = false;
            }
        }

        private void pagesDynamicContinuesIndexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pagesDynamicContinuesIndexCheckBox.Checked)
            {
                pagesDynamicContinuesIndexNameTextBox.Enabled = true;
            }
            else
            {
                pagesDynamicContinuesIndexNameTextBox.Enabled = false;
            }
            if (pagesDynamicContinuesIndexCheckBox.Checked || pagesDynamicContinuesCatCheckBox.Checked)
            {
                pagesDynamicContinuesKeysOnPageNumericUpDown.Enabled = true;
            }
            else
            {
                pagesDynamicContinuesKeysOnPageNumericUpDown.Enabled = false;
            }
        }

        private void pagesDynamicContinuesCatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pagesDynamicContinuesCatCheckBox.Checked)
            {
                pagesDynamicContinuesCatName1TextBox.Enabled = true;
                pagesDynamicContinuesCatName2TextBox.Enabled = true;
            }
            else
            {
                pagesDynamicContinuesCatName1TextBox.Enabled = false;
                pagesDynamicContinuesCatName2TextBox.Enabled = false;
            }
            if (pagesDynamicContinuesIndexCheckBox.Checked || pagesDynamicContinuesCatCheckBox.Checked)
            {
                pagesDynamicContinuesKeysOnPageNumericUpDown.Enabled = true;
            }
            else
            {
                pagesDynamicContinuesKeysOnPageNumericUpDown.Enabled = false;
            }
        }

        private void siteMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (siteMapCheckBox.Checked)
            {
                siteMapTypeComboBox.Enabled = true;
                if (siteMapTypeComboBox.SelectedIndex == 0)
                {
                    siteMapHTMLGroupBox.Enabled = false;
                }
                else if(siteMapTypeComboBox.SelectedIndex == 1)
                {
                    siteMapHTMLGroupBox.Enabled = true;
                }
            }
            else
            {
                siteMapTypeComboBox.Enabled = false;
                siteMapHTMLGroupBox.Enabled = false;
            }
        }

        private void siteMapTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siteMapTypeComboBox.SelectedIndex == -1)
            {
                return;
            }

            if (siteMapCheckBox.Checked)
            {
                if (siteMapTypeComboBox.SelectedIndex == 0 || siteMapTypeComboBox.SelectedIndex == 3 || siteMapTypeComboBox.SelectedIndex == 4)
                {
                    siteMapHTMLGroupBox.Enabled = false;
                }
                else
                {
                    siteMapHTMLGroupBox.Enabled = true;
                    if (siteMapHTMLComboBox.SelectedIndex == 0)
                    {
                        siteMapHTMLLinksMinNumericUpDown.Enabled = false;
                        siteMapHTMLLinksMaxNumericUpDown.Enabled = false;
                    }
                    else if (siteMapHTMLComboBox.SelectedIndex == 1)
                    {
                        siteMapHTMLLinksMinNumericUpDown.Enabled = true;
                        siteMapHTMLLinksMaxNumericUpDown.Enabled = true;
                    }
                }
            }
        }

        private void siteMapHTMLComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siteMapHTMLComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (siteMapHTMLComboBox.SelectedIndex == 0)
            {
                siteMapHTMLLinksMinNumericUpDown.Enabled = false;
                siteMapHTMLLinksMaxNumericUpDown.Enabled = false;
                siteMapHTMLNameTextBox.Text = "map";
            }
            else
            {
                siteMapHTMLLinksMinNumericUpDown.Enabled = true;
                siteMapHTMLLinksMaxNumericUpDown.Enabled = true;
                siteMapHTMLNameTextBox.Text = "map-[N]";
            }
        }

        private void robotsCreateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (robotsCreateCheckBox.Checked)
            {
                robotsComboBox.Enabled = true;
                if (robotsComboBox.SelectedIndex == 0)
                {
                    MakeRobots();
                    robotsTextBox.Enabled = false;
                }
                else if (robotsComboBox.SelectedIndex == 1)
                {
                    robotsTextBox.Enabled = true;
                }
            }
            else
            {
                robotsComboBox.Enabled = false;
                robotsTextBox.Enabled = false;
            }
        }

        private void MakeRobots()
        {
            if (generalWSComboBox.SelectedIndex != -1 && generalTemplateComboBox.SelectedIndex != -1)
            {
                StringBuilder robots = new StringBuilder(200);
                robots.Append("User-agent: *\r\n");

                List<string> folders = new List<string>();
                for (int i = 0; i < SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Files.Count; i++)
                {
                    if (SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Files[i].Substring(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Files[i].IndexOf("Files\\") + 6).Contains("\\"))
                    {
                        bool found = false;
                        string folderPath = SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Files[i].Substring(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Files[i].IndexOf("Files\\") + 6);
                        folderPath = folderPath.Substring(0, folderPath.LastIndexOf("\\") + 1);
                        for (int k = 0; k < folders.Count; k++)
                        {
                            if (folders[k] == folderPath)
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            folders.Add(folderPath);
                            robots.Append("Disallow: /" + folderPath.Replace("\\", "/") + "\r\n");
                        }
                    }
                }
                for (int i = 0; i < SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Images.Count; i++)
                {
                    if (!SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Images[i].StartsWith("http"))
                    {
                        if (SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Images[i].Substring(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Images[i].IndexOf("Files\\") + 6).Contains("\\"))
                        {
                            bool found = false;
                            string folderPath = SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Images[i].Substring(SharedData.WorkSpaces[generalWSComboBox.SelectedIndex].Templates[generalTemplateComboBox.SelectedIndex].Images[i].IndexOf("Files\\") + 6);
                            folderPath = folderPath.Substring(0, folderPath.LastIndexOf("\\") + 1);
                            for (int k = 0; k < folders.Count; k++)
                            {
                                if (folders[k] == folderPath)
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                folders.Add(folderPath);
                                robots.Append("Disallow: /" + folderPath.Replace("\\", "/") + "\r\n");
                            }
                        }
                    }
                }
                if (siteMapCheckBox.Checked && generalDoorwaysLinksTextBox.Text != string.Empty && (siteMapTypeComboBox.SelectedIndex == 0 || siteMapTypeComboBox.SelectedIndex == 2))
                {
                    robots.Append("Sitemap: [SITEMAP]");
                }
                robotsTextBox.Text = robots.ToString();
            }
        }

        private void robotsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (robotsComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (robotsComboBox.SelectedIndex == 0)
            {
                robotsTextBox.Enabled = false;
                MakeRobots();
            }
            else
            {
                robotsTextBox.Enabled = true;
            }
        }

        private void textGeneratingMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (textGeneratingMethodComboBox.SelectedIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        textGeneratingChainGraphsGroupBox.Enabled = true;
                        textGenerationMixRandAsIsGroupBox.Enabled = false;
                        if (textGeneratingMethodComboBox.SelectedIndex == 0)
                        {
                            textGeneratingChainTAWordsComboBox.SelectedIndex = 0;
                            textGeneratingChainTAWordsComboBox.Enabled = false;
                            textGeneratingChainConstructionComboBox.SelectedIndex = 0;
                            textGeneratingChainConstructionComboBox.Enabled = false;
                            textGeneratingChainConstructionProbabilityCheckBox.Checked = false;
                            textGeneratingChainConstructionProbabilityCheckBox.Enabled = false;
                        }
                        else
                        {
                            if (textGeneratingMethodComboBox.SelectedIndex == 2)
                            {
                                textGeneratingChainTAWordsComboBox.SelectedIndex = 0;
                                textGeneratingChainTAWordsComboBox.Enabled = false;
                            }
                            else
                            {
                                textGeneratingChainTAWordsComboBox.Enabled = true;
                            }
                            textGeneratingChainConstructionComboBox.Enabled = true;
                            textGeneratingChainConstructionProbabilityCheckBox.Enabled = true;
                        }
                        if (textGeneratingMethodComboBox.SelectedIndex == 3 || textGeneratingMethodComboBox.SelectedIndex == 4)
                        {
                            textGeneratingChainConstructionProbabilityCheckBox.Checked = false;
                            textGeneratingChainConstructionProbabilityCheckBox.Enabled = false;
                        }
                        break;
                    }
                case 5:
                case 6:
                case 7:
                    {
                        if (textGeneratingMethodComboBox.SelectedIndex == 5)
                        {
                            textGenerationMixRandAsIsRadiusNumericUpDown.Enabled = true;
                        }
                        else
                        {
                            textGenerationMixRandAsIsRadiusNumericUpDown.Enabled = false;
                        }

                        textGeneratingChainGraphsGroupBox.Enabled = false;
                        textGenerationMixRandAsIsGroupBox.Enabled = true;
                        if (textGeneratingMethodComboBox.SelectedIndex == 7)
                        {
                            textGenerationMixRandAsIsTypeComboBox.SelectedIndex = 0;
                            textGenerationMixRandAsIsTypeComboBox.Enabled = false;
                        }
                        else
                        {
                            textGenerationMixRandAsIsTypeComboBox.Enabled = true;
                        }

                        if (textGeneratingMethodComboBox.SelectedIndex == 5 || textGeneratingMethodComboBox.SelectedIndex == 7)
                        {
                            textGenerationMixRandAsIsConstructionComboBox.SelectedIndex = 0;
                            textGenerationMixRandAsIsConstructionComboBox.Enabled = false;
                        }
                        else
                        {
                            textGenerationMixRandAsIsConstructionComboBox.Enabled = true;
                        }

                        break;
                    }
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    {
                        textGeneratingChainGraphsGroupBox.Enabled = false;
                        textGenerationMixRandAsIsGroupBox.Enabled = false;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void textGeneratingKoPMoreThanOneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textGeneratingKoPMoreThanOneCheckBox.Checked)
            {
                textGeneratingKoPMinNumericUpDown.Enabled = true;
                textGeneratingKoPMaxNumericUpDown.Enabled = true;
                textGeneratingKeywordsPfEKCheckBox.Enabled = true;
            }
            else
            {
                textGeneratingKoPMinNumericUpDown.Enabled = false;
                textGeneratingKoPMaxNumericUpDown.Enabled = false;
                textGeneratingKeywordsPfEKCheckBox.Enabled = false;
            }
        }

        private void textGeneratingKeywordsInsertComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textGeneratingKeywordsInsertComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (textGeneratingKeywordsInsertComboBox.SelectedIndex != 1)
            {
                textGeneratingKeywordsInsertNumericUpDown.Enabled = false;
            }
            else
            {
                textGeneratingKeywordsInsertNumericUpDown.Enabled = true;
            }
        }

        private void textGeneratingKeywordsInsertOtherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textGeneratingKeywordsInsertOtherCheckBox.Checked)
            {
                textGeneratingKeywordsInsertOtherNumericUpDown.Enabled = true;
            }
            else
            {
                textGeneratingKeywordsInsertOtherNumericUpDown.Enabled = false;
            }
        }

        private void textGeneratingSelectionKeywordsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textGeneratingSelectionKeywordsCheckBox.Checked)
            {
                textGeneratingSelectionKeywordsTextBox.Enabled = true;
            }
            else
            {
                textGeneratingSelectionKeywordsTextBox.Enabled = false;
            }
            if (textGeneratingSelectionKeywordsCheckBox.Checked || textGeneratingSelectionPhrasesCheckBox.Checked)
            {
                textGeneratingSelectionNumericUpDown.Enabled = true;
            }
            else
            {
                textGeneratingSelectionNumericUpDown.Enabled = false;
            }
        }

        private void textGeneratingSelectionPhrasesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textGeneratingSelectionPhrasesCheckBox.Checked)
            {
                textGeneratingSelectionPhrasesTextBox.Enabled = true;
            }
            else
            {
                textGeneratingSelectionPhrasesTextBox.Enabled = false;
            }
            if (textGeneratingSelectionKeywordsCheckBox.Checked || textGeneratingSelectionPhrasesCheckBox.Checked)
            {
                textGeneratingSelectionNumericUpDown.Enabled = true;
            }
            else
            {
                textGeneratingSelectionNumericUpDown.Enabled = false;
            }
        }

        private void textGeneratingPunctuationInsertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textGeneratingPunctuationInsertCheckBox.Checked)
            {
                textGeneratingPunctuationInsertMinNumericUpDown.Enabled = true;
                textGeneratingPunctuationInsertMaxNumericUpDown.Enabled = true;
                textGeneratingPunctuationTextBox.Enabled = true;
            }
            else
            {
                textGeneratingPunctuationInsertMinNumericUpDown.Enabled = false;
                textGeneratingPunctuationInsertMaxNumericUpDown.Enabled = false;
                textGeneratingPunctuationTextBox.Enabled = false;
            }
        }

        private void textGeneratingSentencesLengthСomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textGeneratingSentencesLengthTypeСomboBox.SelectedIndex == -1)
            {
                return;
            }
            if (textGeneratingSentencesLengthTypeСomboBox.SelectedIndex == 0)
            {
                textGeneratingSentencesWordsNumericUpDown.Enabled = false;
                textGeneratingSentencesWordsStepNumericUpDown.Enabled = false;

                textGeneratingSentencesLengthNumericUpDown.Enabled = true;
            }
            else
            {
                textGeneratingSentencesWordsNumericUpDown.Enabled = true;
                textGeneratingSentencesWordsStepNumericUpDown.Enabled = true;

                textGeneratingSentencesLengthNumericUpDown.Enabled = false;
            }

        }

        private void textGeneratingParagraphsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textGeneratingParagraphsCheckBox.Checked)
            {
                textGeneratingParagraphsMinNumericUpDown.Enabled = true;
                textGeneratingParagraphsMaxNumericUpDown.Enabled = true;
            }
            else
            {
                textGeneratingParagraphsMinNumericUpDown.Enabled = false;
                textGeneratingParagraphsMaxNumericUpDown.Enabled = false;
            }
        }

        private void textGeneratingChainTAWordsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textGeneratingChainTAWordsComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (textGeneratingChainTAWordsComboBox.SelectedIndex == 0)
            {
                textGeneratingChainTAIfLongerNumericUpDown.Enabled = false;
            }
            else
            {
                textGeneratingChainTAIfLongerNumericUpDown.Enabled = true;
            }
        }

        private void textGeneratingChainConstructionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textGeneratingChainConstructionComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (textGeneratingChainConstructionComboBox.SelectedIndex == 0)
            {
                textGeneratingChainConstructionInsertMinNumericUpDown.Enabled = false;
                textGeneratingChainConstructionInsertMaxNumericUpDown.Enabled = false;
            }
            else
            {
                textGeneratingChainConstructionInsertMinNumericUpDown.Enabled = true;
                textGeneratingChainConstructionInsertMaxNumericUpDown.Enabled = true;
            }
        }

        private void textGenerationMixRandAsIsConstructionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textGenerationMixRandAsIsConstructionComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (textGenerationMixRandAsIsConstructionComboBox.SelectedIndex == 0)
            {
                textGenerationMixRandAsIsConstructionInsertMinNumericUpDown.Enabled = false;
                textGenerationMixRandAsIsConstructionInsertMaxNumericUpDown.Enabled = false;
            }
            else
            {
                textGenerationMixRandAsIsConstructionInsertMinNumericUpDown.Enabled = true;
                textGenerationMixRandAsIsConstructionInsertMaxNumericUpDown.Enabled = true;
            }
        }

        private void macrosesMainLinkComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (macrosesMainLinkComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (macrosesMainLinkComboBox.SelectedIndex == 3)
            {
                macrosesMainLinkTextBox.Enabled = true;
            }
            else
            {
                macrosesMainLinkTextBox.Enabled = false;
            }
        }

        private void entranceGeneralInsertTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entranceGeneralInsertTypeComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (entranceGeneralInsertTypeComboBox.SelectedIndex == 1)
            {
                entranceGeneralJSFilesEncriptCheckBox.Enabled = true;
            }
            else if (entranceGeneralInsertTypeComboBox.SelectedIndex == 2)
            {
                entranceGeneralJSFilesEncriptCheckBox.Enabled = true;
            }
            else
            {
                entranceGeneralJSFilesEncriptCheckBox.Enabled = false;
            }
            UpdateRedirectTextBox();
        }

        private void linksInternalCreateСheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (linksInternalCreateСheckBox.Checked)
            {
                linksInternalAnchorsСomboBox.Enabled = true;
                if (linksInternalAnchorsСomboBox.SelectedIndex == 1 || linksInternalAnchorsСomboBox.SelectedIndex == 2)
                {
                    linksInternalLinksLengthMinNumericUpDown.Enabled = true;
                    linksInternalLinksLengthMaxNumericUpDown.Enabled = true;
                }
                else
                {
                    linksInternalLinksLengthMinNumericUpDown.Enabled = false;
                    linksInternalLinksLengthMaxNumericUpDown.Enabled = false;
                }
            }
            else
            {
                linksInternalAnchorsСomboBox.Enabled = false;
                linksInternalLinksLengthMinNumericUpDown.Enabled = false;
                linksInternalLinksLengthMaxNumericUpDown.Enabled = false;
            }
        }

        private void linksInternalAnchorsСomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (linksInternalAnchorsСomboBox.SelectedIndex)
            {
                case 1:
                case 2:
                    {
                        linksInternalLinksLengthMinNumericUpDown.Enabled = true;
                        linksInternalLinksLengthMaxNumericUpDown.Enabled = true;
                        break;
                    }
                default:
                    {
                        linksInternalLinksLengthMinNumericUpDown.Enabled = false;
                        linksInternalLinksLengthMaxNumericUpDown.Enabled = false;
                        return;
                    }
            }
        }

        private void linksExternalUseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            linksExternalTabControl.Enabled = linksExternalUseCheckBox.Checked;
            linksExternalNetBlockLoadButton.Enabled = linksExternalUseCheckBox.Checked;
            if (!linksExternalUseCheckBox.Checked)
            {
                linksExternalInTextCheckBox.Checked = false;
            }
        }

        private void spamCreateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (spamCreateCheckBox.Checked)
            {
                spamTypesGroupBox.Enabled = true;
                spamSaveGroupBox.Enabled = true;
            }
            else
            {
                spamTypesGroupBox.Enabled = false;
                spamSaveGroupBox.Enabled = false;
            }
        }

        private void ftpGeneralUploadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ftpGeneralUploadCheckBox.Checked)
            {
                ftpGeneralUploadTypeComboBox.Enabled = true;
                if (ftpGeneralUploadTypeComboBox.SelectedIndex == 0)
                {
                    ftpGeneralUploadSettingsUploadSaveToTextBox.Enabled = false;
                    ftpGeneralUploadSettingsUploadSaveToButton.Enabled = false;

                    ftpGeneralDeleteAfterUploadСheckBox.Enabled = true;
                    if (generalArchiveComboBox.SelectedIndex == 0)
                    {
                        ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = true;
                    }
                    else
                    {
                        ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = false;
                    }
                    if (ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex == 0)
                    {
                        ftpGeneralUploadSettingsUploadArchiveTextBox.Enabled = false;
                    }
                    else
                    {
                        ftpGeneralUploadSettingsUploadArchiveTextBox.Enabled = true;
                    }
                    ftpGeneralUploadSettingsGroupBox.Enabled = true;
                }
                else
                {
                    ftpGeneralUploadSettingsGroupBox.Enabled = false;
                }
                ftpGeneralSettingsDataGridView.Enabled = true;
            }
            else
            {
                ftpGeneralUploadTypeComboBox.Enabled = false;
                ftpGeneralUploadSettingsUploadSaveToTextBox.Enabled = false;
                ftpGeneralUploadSettingsUploadSaveToButton.Enabled = false;

                ftpGeneralUploadSettingsGroupBox.Enabled = false;
            }
        }

        private void ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ftpGeneralUploadCheckBox.Checked && ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex != 0)
            {
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 1;
            }
            else if (generalArchiveComboBox.SelectedIndex == 0)
            {
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 10;
            }
            else
            {
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 1;
            }
        }

        private void pagesStaticPagesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pagesStaticPagesCheckBox.Checked)
            {
                pagesStaticPagesTextBox.Enabled = true;
                if (pagesDoorwayTypeComboBox.SelectedIndex == 1)
                {
                    pagesDynamicNamesStaticPageTextBox.Enabled = true;
                }
                else
                {
                    pagesDynamicNamesStaticPageTextBox.Enabled = false;
                }
            }
            else
            {
                pagesStaticPagesTextBox.Enabled = false;
                pagesDynamicNamesStaticPageTextBox.Enabled = false;
            }
        }

        #region Menu
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveTextFileDialog.FileName = string.Empty;
            saveTextFileDialog.ShowDialog();
            if (saveTextFileDialog.FileName == string.Empty)
            {
                return;
            }
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, generalDoorwaysLinksTextBox.Text, Encoding.Default);
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, categoryStaticCategoriesTextBox.Text, Encoding.Default);
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, pagesStaticPagesTextBox.Text, Encoding.Default);
                        break;
                    }
                case "robotsTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, robotsTextBox.Text, Encoding.Default);
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, macrosesMainLinkTextBox.Text, Encoding.Default);
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, macrosesSiteTextBox.Text, Encoding.Default);
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, macrosesTitleTextBox.Text, Encoding.Default);
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, entranceRedirectFrameTextBox.Text, Encoding.Default);
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, entranceCounterTextBox.Text, Encoding.Default);
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, linksExternalTextBox.Text, Encoding.Default);
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        File.WriteAllText(saveTextFileDialog.FileName, spamTypesTextBox.Text, Encoding.Default);
                        break;
                    }
            }
        }

        private void loadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (openTextFileDialog.FileName == string.Empty)
            {
                return;
            }
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        generalDoorwaysLinksTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        categoryStaticCategoriesTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        pagesStaticPagesTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "robotsTextBox":
                    {
                        robotsTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        macrosesMainLinkTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        macrosesSiteTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        macrosesTitleTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        entranceRedirectFrameTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        entranceCounterTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        linksExternalTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        spamTypesTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                        break;
                    }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        Clipboard.SetText(generalDoorwaysLinksTextBox.Text);
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        Clipboard.SetText(categoryStaticCategoriesTextBox.Text);
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        Clipboard.SetText(pagesStaticPagesTextBox.Text);
                        break;
                    }
                case "robotsTextBox":
                    {
                        Clipboard.SetText(robotsTextBox.Text);
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        Clipboard.SetText(macrosesMainLinkTextBox.Text);
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        Clipboard.SetText(macrosesSiteTextBox.Text);
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        Clipboard.SetText(macrosesTitleTextBox.Text);
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        Clipboard.SetText(entranceRedirectFrameTextBox.Text);
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        Clipboard.SetText(entranceCounterTextBox.Text);
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        Clipboard.SetText(linksExternalTextBox.Text);
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        Clipboard.SetText(spamTypesTextBox.Text);
                        break;
                    }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        Clipboard.SetText(generalDoorwaysLinksTextBox.Text);
                        generalDoorwaysLinksTextBox.Text = string.Empty;
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        Clipboard.SetText(categoryStaticCategoriesTextBox.Text);
                        categoryStaticCategoriesTextBox.Text = string.Empty;
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        Clipboard.SetText(pagesStaticPagesTextBox.Text);
                        pagesStaticPagesTextBox.Text = string.Empty;
                        break;
                    }
                case "robotsTextBox":
                    {
                        Clipboard.SetText(robotsTextBox.Text);
                        robotsTextBox.Text = string.Empty;
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        Clipboard.SetText(macrosesMainLinkTextBox.Text);
                        macrosesMainLinkTextBox.Text = string.Empty;
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        Clipboard.SetText(macrosesSiteTextBox.Text);
                        macrosesSiteTextBox.Text = string.Empty;
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        Clipboard.SetText(macrosesTitleTextBox.Text);
                        macrosesTitleTextBox.Text = string.Empty;
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        Clipboard.SetText(entranceRedirectFrameTextBox.Text);
                        entranceRedirectFrameTextBox.Text = string.Empty;
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        Clipboard.SetText(entranceCounterTextBox.Text);
                        entranceCounterTextBox.Text = string.Empty;
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        Clipboard.SetText(linksExternalTextBox.Text);
                        linksExternalTextBox.Text = string.Empty;
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        Clipboard.SetText(spamTypesTextBox.Text);
                        spamTypesTextBox.Text = string.Empty;
                        break;
                    }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        generalDoorwaysLinksTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        categoryStaticCategoriesTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        pagesStaticPagesTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "robotsTextBox":
                    {
                        robotsTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        macrosesMainLinkTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        macrosesSiteTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        macrosesTitleTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        entranceRedirectFrameTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        entranceCounterTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        linksExternalTextBox.Text = Clipboard.GetText();
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        spamTypesTextBox.Text = Clipboard.GetText();
                        break;
                    }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        generalDoorwaysLinksTextBox.SelectAll();
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        categoryStaticCategoriesTextBox.SelectAll();
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        pagesStaticPagesTextBox.SelectAll();
                        break;
                    }
                case "robotsTextBox":
                    {
                        robotsTextBox.SelectAll();
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        macrosesMainLinkTextBox.SelectAll();
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        macrosesSiteTextBox.SelectAll();
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        macrosesTitleTextBox.SelectAll();
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        entranceRedirectFrameTextBox.SelectAll();
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        entranceCounterTextBox.SelectAll();
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        linksExternalTextBox.SelectAll();
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        spamTypesTextBox.SelectAll();
                        break;
                    }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (standardContextMenuStrip.SourceControl.Name)
            {
                case "generalDoorwaysLinksTextBox":
                    {
                        generalDoorwaysLinksTextBox.Text = string.Empty;
                        break;
                    }
                case "categoryStaticCategoriesTextBox":
                    {
                        categoryStaticCategoriesTextBox.Text = string.Empty;
                        break;
                    }
                case "pagesStaticPagesTextBox":
                    {
                        pagesStaticPagesTextBox.Text = string.Empty;
                        break;
                    }
                case "robotsTextBox":
                    {
                        robotsTextBox.Text = string.Empty;
                        break;
                    }
                case "macrosesMainLinkTextBox":
                    {
                        macrosesMainLinkTextBox.Text = string.Empty;
                        break;
                    }
                case "macrosesSiteTextBox":
                    {
                        macrosesSiteTextBox.Text = string.Empty;
                        break;
                    }
                case "macrosesTitleTextBox":
                    {
                        macrosesTitleTextBox.Text = string.Empty;
                        break;
                    }
                case "entranceRedirectFrameTextBox":
                    {
                        entranceRedirectFrameTextBox.Text = string.Empty;
                        break;
                    }
                case "entranceCounterTextBox":
                    {
                        entranceCounterTextBox.Text = string.Empty;
                        break;
                    }
                case "linksExternalTextBox":
                    {
                        linksExternalTextBox.Text = string.Empty;
                        break;
                    }
                case "spamTypesTextBox":
                    {
                        spamTypesTextBox.Text = string.Empty;
                        break;
                    }
            }
        }
        #endregion

        private void generalArchiveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (generalArchiveComboBox.SelectedIndex == 0)
            {
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 10;
                if (ftpGeneralUploadCheckBox.Checked)
                {
                    ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = true;
                }
                else
                {
                    ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = false;
                }
                generalArchiveTextBox.Text = string.Empty;
                generalArchiveTextBox.Enabled = false;
            }
            else
            {
                generalArchiveTextBox.Enabled = true;
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 1;
                ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex = 0;
                ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = false;
                if (generalArchiveComboBox.SelectedIndex == 1)
                {
                    generalArchiveTextBox.Text = "doorway.zip";
                }
                else
                {
                    generalArchiveTextBox.Text = "doorway.tar.gz";
                }
            }
        }

        private void ftpGeneralUploadSettingsUploadArchiveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex == 0)
            {
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 10;
                ftpGeneralUploadSettingsUploadArchiveTextBox.Text = string.Empty;
                ftpGeneralUploadSettingsUploadArchiveTextBox.Enabled = false;
            }
            else
            {
                ftpGeneralUploadSettingsThreadsPerUploadNumericUpDown.Maximum = 1;
                ftpGeneralUploadSettingsUploadArchiveTextBox.Enabled = true;
                if (ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex == 1)
                {
                    ftpGeneralUploadSettingsUploadArchiveTextBox.Text = "doorway.zip";
                }
                else
                {
                    ftpGeneralUploadSettingsUploadArchiveTextBox.Text = "doorway.tar.gz";
                }
            }
        }

        private void entranceGeneralTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entranceGeneralTypeComboBox.SelectedIndex == 2)
            {
                entranceGeneralAcceptorAdressComboBox.Enabled = false;
                entranceGeneralAcceptorAdressTextBox.Enabled = false;
            }
            else
            {
                entranceGeneralAcceptorAdressComboBox.Enabled = true;
                entranceGeneralAcceptorAdressTextBox.Enabled = true;
            }
            UpdateRedirectTextBox();
        }

        private void entranceGeneralAcceptorAdressTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateRedirectTextBox();
        }

        private void entranceGeneralAcceptorAdressComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRedirectTextBox();
        }

        private void UpdateRedirectTextBox()
        {
            switch (entranceGeneralTypeComboBox.SelectedIndex)
            {
                //Redirect
                case 0:
                    {
                        if (entranceGeneralAcceptorAdressComboBox.SelectedIndex == 0)
                        {
                            entranceRedirectFrameTextBox.Text = "<script language='javascript'>document.location='" + entranceGeneralAcceptorAdressTextBox.Text + "';</script>";
                        }
                        else
                        {
                            entranceRedirectFrameTextBox.Text = "<script language='javascript'>document.location='" + entranceGeneralAcceptorAdressTextBox.Text + "[PLUSKEYWORD]';</script>";
                        }
                        break;
                    }
                //Frame
                case 1:
                    {
                        if (entranceGeneralAcceptorAdressComboBox.SelectedIndex == 0)
                        {
                            entranceRedirectFrameTextBox.Text = "<script language='javascript'>var Addr = '" + entranceGeneralAcceptorAdressTextBox.Text + "';document.write('<iframe src=' + Addr + ' width=\"100%\" height=\"100%\" frameborder=\"NO\" border=\"0\" framespacing=\"0\" scrolling=\"NO\"></iframe>');document.write(\"<script language='javascript'>document.delete();<script>\");</script>";
                        }
                        else
                        {
                            entranceRedirectFrameTextBox.Text = "<script language='javascript'>var Addr = '" + entranceGeneralAcceptorAdressTextBox.Text + "[PLUSKEYWORD]';document.write('<iframe src=' + Addr + ' width=\"100%\" height=\"100%\" frameborder=\"NO\" border=\"0\" framespacing=\"0\" scrolling=\"NO\"></iframe>');document.write(\"<script language='javascript'>document.delete();<script>\");</script>";
                        }
                        break;
                    }
                //Other
                case 2:
                    {
                        entranceRedirectFrameTextBox.Text = string.Empty;
                        break;
                    }
            }
        }

        private void ftpGeneralUploadTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ftpGeneralUploadCheckBox.Checked)
            {
                if (ftpGeneralUploadTypeComboBox.SelectedIndex == 0)
                {
                    ftpGeneralUploadSettingsUploadSaveToTextBox.Enabled = false;
                    ftpGeneralUploadSettingsUploadSaveToButton.Enabled = false;

                    ftpGeneralUploadSettingsGroupBox.Enabled = true;

                    if (generalArchiveComboBox.SelectedIndex == 0)
                    {
                        ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = true;
                    }
                    else
                    {
                        ftpGeneralUploadSettingsUploadArchiveComboBox.Enabled = false;
                    }
                    if (ftpGeneralUploadSettingsUploadArchiveComboBox.SelectedIndex == 0)
                    {
                        ftpGeneralUploadSettingsUploadArchiveTextBox.Enabled = false;
                    }
                    else
                    {
                        ftpGeneralUploadSettingsUploadArchiveTextBox.Enabled = true;
                    }
                }
                else
                {
                    ftpGeneralUploadSettingsUploadSaveToTextBox.Enabled = true;
                    ftpGeneralUploadSettingsUploadSaveToButton.Enabled = true;

                    ftpGeneralUploadSettingsGroupBox.Enabled = false;
                }
            }
        }

        private void ftpGeneralUploadSettingsUploadSaveToButton_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = string.Empty;
            saveToFolderBrowserDialog.ShowDialog();
            if (saveToFolderBrowserDialog.SelectedPath == string.Empty)
            {
                return;
            }
            ftpGeneralUploadSettingsUploadSaveToTextBox.Text = saveToFolderBrowserDialog.SelectedPath;
        }

        private void pagesCreateRSSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pagesRSSNumericUpDown.Enabled = pagesCreateRSSCheckBox.Checked;
            pagesRSSFileName.Enabled = pagesCreateRSSCheckBox.Checked;
        }

        private void spamSaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (spamSaveCheckBox.Checked)
            {
                spamSaveTypeComboBox.Enabled = true;
                spamSavePathTextBox.Enabled = true;
                spamSaveSelectPathButton.Enabled = true;
                spamSaveEncodingComboBox.Enabled = true;
            }
            else
            {
                spamSaveTypeComboBox.Enabled = false;
                spamSavePathTextBox.Enabled = false;
                spamSaveSelectPathButton.Enabled = false;
                spamSaveEncodingComboBox.Enabled = false;
            }
        }

        private void spamSaveSelectPathButton_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = string.Empty;
            saveToFolderBrowserDialog.ShowDialog();
            if (saveToFolderBrowserDialog.SelectedPath == string.Empty)
            {
                return;
            }
            spamSavePathTextBox.Text = saveToFolderBrowserDialog.SelectedPath;
        }

        private void ftpClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ftpGeneralSettingsDataGridView.Rows.Clear();
        }

        private void ftpImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (openTextFileDialog.FileName == string.Empty)
            {
                return;
            }
            try
            {
                //Loading
                string[] ftpData = File.ReadAllLines(openTextFileDialog.FileName, Encoding.UTF8);
                //Working
                for (int i = 0; i < ftpData.Length; i++)
                {
                    try
                    {
                        string[] rowData = ftpData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (rowData.Length == 4 || rowData.Length == 3)
                        {
                            ftpGeneralSettingsDataGridView.Rows.Add();
                            for (int k = 0; k < rowData.Length; k++)
                            {
                                ftpGeneralSettingsDataGridView.Rows[ftpGeneralSettingsDataGridView.Rows.Count - 2].Cells[k].Value = rowData[k];
                            }
                            if (rowData.Length == 3)
                            {
                                ftpGeneralSettingsDataGridView.Rows[ftpGeneralSettingsDataGridView.Rows.Count - 2].Cells[3].Value = string.Empty;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                ftpGeneralSettingsDataGridView.Rows.Clear();
            }
        }

        private void ftpToolsLabel_Click(object sender, EventArgs e)
        {
            ftpContextMenuStrip.Show(ftpToolsLabel,new Point(0,0));
        }

        private void generalImagesNamingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (generalImagesNamingComboBox.SelectedIndex == 0)
            {
                generalImagesNamingTextBox.Enabled = false;
                generalImagesNamingButton.Enabled = false;
            }
            else
            {
                generalImagesNamingTextBox.Enabled = true;
                generalImagesNamingButton.Enabled = true;
            }
        }

        private void generalImagesNamingButton_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (openTextFileDialog.FileName == string.Empty)
            {
                return;
            }
            generalImagesNamingTextBox.Text = openTextFileDialog.FileName;
        }

        private void tagsActionsFileSelectButton_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (openTextFileDialog.FileName == string.Empty)
            {
                return;
            }

            tagsActionsFileTextBox.Text = openTextFileDialog.FileName;
        }

        private void tagsActionsAddButton_Click(object sender, EventArgs e)
        {
            if (tagsActionsFileTextBox.Text != string.Empty)
            {
                // Adding
                tagsDataGridView.Rows.Add();

                tagsDataGridView.Rows[tagsDataGridView.Rows.Count - 1].Cells[0].Value = tagsActionsFileTextBox.Text;
                tagsDataGridView.Rows[tagsDataGridView.Rows.Count - 1].Cells[1].Value = tagsActionsEncodingComboBox.SelectedIndex.ToString();
                
                // Clear fields
                tagsActionsFileTextBox.Text = string.Empty;
                tagsActionsEncodingComboBox.SelectedIndex = 0;
            }
        }

        private void tagsActionsRemoveButton_Click(object sender, EventArgs e)
        {
            if (tagsDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRow = tagsDataGridView.SelectedRows;
                tagsDataGridView.Rows.Remove(selectedRow[0]);
            }
        }

        private void XrumerOpenDirectoryButton_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = string.Empty;
            if (saveToFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                XrumerDirectoryTextBox.Text = saveToFolderBrowserDialog.SelectedPath;
            }
        }

        private void generalDoorwaysLinksTextBox_TextChanged(object sender, EventArgs e)
        {
            if (generalDoorwaysLinksTextBox.Text.Length != 0 && !XrumerUseCheckBox.Checked)
            {
                XrumerUseCheckBox.Enabled = true;
            }
            else if (generalDoorwaysLinksTextBox.Text.Length == 0 && XrumerUseCheckBox.Checked)
            {
                XrumerUseCheckBox.Enabled = false;
                XrumerUseCheckBox.Checked = false;
            }
        }

        private void generalDoorwaysLinksTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = true;
                generalDoorwaysLinksTextBox.SelectAll();
            }
        }

        private void XrumerUseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = XrumerUseCheckBox.Enabled && XrumerUseCheckBox.Checked;

            XrumerDirectoryTextBox.Enabled = enabled;
            XrumerOpenDirectoryButton.Enabled = enabled;
            XrumerTemplateGroupBox.Enabled = enabled;
            XrumerTextGroupBox.Enabled = enabled;
        }

        private void XrumerTemplateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = true;
                XrumerTemplateTextBox.SelectAll();
            }
        }

        private void XrumerApplyDefaultButton_Click(object sender, EventArgs e)
        {
            XrumerTemplateTextBox.Text = PresetSettings.XRumerDefaultTemplate;
        }

        private void XrumerTemplateOpenButton_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            if (openTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XrumerTemplateTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                }
                catch (Exception) { }
            }
        }

        private void XrumerTextTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = true;
                XrumerTextTextBox.SelectAll();
            }
        }

        private void XrumerTextOpenButton_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            if (openTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XrumerTextTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                }
                catch (Exception) { }
            }
        }

        private void linksExternalInTextCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            linksExternalInTextIndexMinNumericUpDown.Enabled = linksExternalInTextCheckBox.Checked;
            linksExternalInTextIndexMaxNumericUpDown.Enabled = linksExternalInTextCheckBox.Checked;
            linksExternalInTextPageMinNumericUpDown.Enabled = linksExternalInTextCheckBox.Checked;
            linksExternalInTextPageMaxNumericUpDown.Enabled = linksExternalInTextCheckBox.Checked;
        }

        private void linksExternalNetBlockLoadButton_Click(object sender, EventArgs e)
        {
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openTextFileDialog.FileName))
            {
                try
                {
                    linksExternalTextBox.Text = File.ReadAllText(openTextFileDialog.FileName);
                }
                catch (Exception)
                {
                    linksExternalTextBox.Text = string.Empty;
                }
            }
        }
    }
}
