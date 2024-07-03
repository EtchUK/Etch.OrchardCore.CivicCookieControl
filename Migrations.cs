using Etch.OrchardCore.Fields.Code.Fields;
using Etch.OrchardCore.Fields.Code.Settings;
using Etch.OrchardCore.Fields.Values.Fields;
using Etch.OrchardCore.Fields.Values.Settings;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Flows.Models;
using OrchardCore.Title.Models;

namespace Etch.OrchardCore.CivicCookieControl
{
    public class Migrations : DataMigration
    {
        private const string RawCookieContentTypeName = "RawCookie";
        private const string PredefinedListEditor = "PredefinedList";
        private const string TextAreaEditor = "TextArea";

        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;

        #endregion

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        #endregion

        #region Migrations

        public int CreateAsync()
        {
            UpdateFrom1Async();

            return 2;
        }

        public int UpdateFrom1Async()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(Models.CivicCookieControl), builder => builder
                .WithField("ApiKey", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("API Key")
                    .WithPosition("1")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = true,
                        Hint = "API key for your CIVIC Cookie Control license."
                    }))
                .WithField("Product", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Product")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("2")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = true,
                        Hint = "Product of your CIVIC Cookie Control license."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select product license",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Community",
                                    Value = "COMMUNITY",
                                },
                                new ListValueOption
                                {
                                    Name = "Pro",
                                    Value = "PRO",
                                },
                                new ListValueOption
                                {
                                    Name = "Pro multi site",
                                    Value = "PRO_MULTISITE",
                                }
                            }
                    }))
                .WithField("InitialState", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Initial State")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("3")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the initial display state of Cookie Control."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "open",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select initial state",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Open",
                                    Value = "open",
                                },
                                new ListValueOption
                                {
                                    Name = "Closed",
                                    Value = "closed",
                                },
                                new ListValueOption
                                {
                                    Name = "Notify",
                                    Value = "notify",
                                },
                                new ListValueOption
                                {
                                    Name = "Top",
                                    Value = "top",
                                },
                                new ListValueOption
                                {
                                    Name = "Box",
                                    Value = "box",
                                }
                            }
                    }))
                .WithField("NotifyOnce", field => field
                    .OfType(nameof(BooleanField))
                    .WithDisplayName("Notify Once")
                    .WithPosition("4")
                    .WithSettings(new BooleanFieldSettings
                    {
                        Hint = "Determines whether the module only shows its initial state once, or if it continues to replay on subsequent page loads until the user has directly interacted with it.",
                        Label = "Notify Once"
                    }))
                .WithField("RejectButton", field => field
                    .OfType(nameof(BooleanField))
                    .WithDisplayName("Reject Button")
                    .WithPosition("5")
                    .WithSettings(new BooleanFieldSettings
                    {
                        Hint = "Determines whether the module shows a reject button alongside the accept button on the notification bar, or alongside the 'accept recommended settings' button when the panel is open.",
                        Label = "Reject Button"
                    }))
                .WithField("AcceptBehaviour", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Accept Behaviour")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("6")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "This property is used to control what will happen when the user clicks on either of the 'Accept' or 'Accept recommended settings' buttons."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "all",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select accept behaviour",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "All",
                                    Value = "all",
                                },
                                new ListValueOption
                                {
                                    Name = "Recommended",
                                    Value = "recommended",
                                }
                            }
                    }))
                 .WithField("CloseOnGlobalChange", field => field
                    .OfType(nameof(BooleanField))
                    .WithDisplayName("Close on Global Change")
                    .WithPosition("7")
                    .WithSettings(new BooleanFieldSettings
                    {
                        DefaultValue = true,
                        Hint = "Determines whether Cookie Control main window will remain open when the user clicks on either of the accept or reject buttons.",
                        Label = "Close on Global Change"
                    }))
                 .WithField("Title", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Title")
                    .WithPosition("8")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Title displayed within cookie control dialog."
                    }))
                 .WithField("Intro", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Intro")
                    .WithEditor(TextAreaEditor)
                    .WithPosition("9")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Content displayed after title within cookie control dialog."
                    }))
                 .WithField("NecessaryTitle", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Necessary Title")
                    .WithPosition("10")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Title displayed for necessary cookies section."
                    }))
                 .WithField("NecessaryDescription", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Necessary Description")
                    .WithEditor(TextAreaEditor)
                    .WithPosition("11")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Explanation of necessary cookies."
                    }))
                 .WithField("ThirdPartyTitle", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Third Party Title")
                    .WithPosition("12")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Title displayed for third party cookies section."
                    }))
                 .WithField("ThirdPartyDescription", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Third Party Description")
                    .WithEditor(TextAreaEditor)
                    .WithPosition("13")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Explanation of third party cookies."
                    }))
                 .WithField("Subdomains", field => field
                    .OfType(nameof(BooleanField))
                    .WithDisplayName("Subdomains")
                    .WithPosition("12")
                    .WithSettings(new BooleanFieldSettings
                    {
                        DefaultValue = true,
                        Hint = "Determines whether Cookie Control's own cookie is saved to the top level domain, and therefore accessible on all sub domains, or disabled and saved only to the request host.",
                        Label = "Subdomains"
                    }))
                 .WithField("NecessaryCookies", field => field
                    .OfType(nameof(ValuesField))
                    .WithDisplayName("Necessary Cookies")
                    .WithPosition("13")
                    .WithSettings(new ValuesFieldSettings
                    {
                        Hint = "This is a list of cookies names necessary for your website's core functionality.",
                    }))
                 .WithField("StatementName", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Statement Name")
                    .WithPosition("14")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Text that you wish to call your terms by, for example Privacy Statement."
                    }))
                 .WithField("StatementDescription", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Statement Description")
                    .WithEditor(TextAreaEditor)
                    .WithPosition("15")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Text description that introduces your Personal Information Policy."
                    }))
                 .WithField("StatementUrl", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Statement URL")
                    .WithPosition("16")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "URL where your terms may be accessed."
                    }))
                 .WithField("StatementUpdated", field => field
                    .OfType(nameof(DateField))
                    .WithDisplayName("Statement Updated")
                    .WithPosition("17")
                    .WithSettings(new DateFieldSettings
                    {
                        Required = false,
                        Hint = "Date that your privacy statement was last issued."
                    }))
                 .WithField("Layout", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Layout")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("18")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the display type and behaviour of Cookie Control."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "slideout",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select layout",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Slideout",
                                    Value = "slideout",
                                },
                                new ListValueOption
                                {
                                    Name = "Popup",
                                    Value = "popup",
                                }
                            }
                    }))
                 .WithField("Position", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Position")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("19")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the side of the display Cookie Control will occupy."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "right",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select position",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Left",
                                    Value = "left",
                                },
                                new ListValueOption
                                {
                                    Name = "Right",
                                    Value = "right",
                                }
                            }
                    }))
                 .WithField("Theme", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Theme")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("20")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the appearance of Cookie Control."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "dark",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select theme",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Dark",
                                    Value = "dark",
                                },
                                new ListValueOption
                                {
                                    Name = "Light",
                                    Value = "light",
                                }
                            }
                    }))
                 .WithField("ToggleType", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Toggle Type")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("21")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the control toggle for each optional cookie."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "slider",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select toggle type",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Slider",
                                    Value = "slider",
                                },
                                new ListValueOption
                                {
                                    Name = "Checkbox",
                                    Value = "checkbox",
                                }
                            }
                    }))
                 .WithField("CloseStyle", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Close Style")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("22")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the closing behaviour of the module."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "icon",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select close style",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Button",
                                    Value = "button",
                                },
                                new ListValueOption
                                {
                                    Name = "Icon",
                                    Value = "icon",
                                },
                                new ListValueOption
                                {
                                    Name = "Labelled",
                                    Value = "labelled",
                                }
                            }
                    }))
                 .WithField("SettingsStyle", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Settings Style")
                    .WithEditor(PredefinedListEditor)
                    .WithPosition("23")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Determines the closing behaviour of the module."
                    })
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        DefaultValue = "button",
                        Editor = EditorOption.Dropdown,
                        Options = new[]
                            {
                                new ListValueOption
                                {
                                    Name = "Select settings style",
                                    Value = "",
                                },
                                new ListValueOption
                                {
                                    Name = "Button",
                                    Value = "button",
                                },
                                new ListValueOption
                                {
                                    Name = "Link",
                                    Value = "link",
                                }
                            }
                    }))
                );

            _contentDefinitionManager.AlterTypeDefinitionAsync(nameof(Models.CivicCookieControl), builder => builder
                .Stereotype("Widget")
                .MergeSettings(JObject.FromObject(new
                {
                    Category = "Content",
                    Description = "Use CIVIC cookie control for users to configure consent.",
                    Icon = "cookie"
                }))
                .DisplayedAs("CIVIC Cookie Control")
                .WithPart(nameof(Models.CivicCookieControl), builder => builder
                    .WithPosition("1"))
                .WithPart("OptionalCookies", nameof(BagPart), builder => builder
                    .WithPosition("2")
                    .WithDisplayName("Optional Cookies")
                    .WithDescription("Collection of cookies used on the site.")
                    .WithSettings(new BagPartSettings
                    {
                        ContainedContentTypes = new string[] { RawCookieContentTypeName }
                    }))
                );

            _contentDefinitionManager.AlterPartDefinitionAsync(RawCookieContentTypeName, builder => builder
                .WithField("Name", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Name")
                    .WithPosition("1"))
                .WithField("Label", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Label")
                    .WithPosition("2"))
                .WithField("Description", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Description")
                    .WithEditor(TextAreaEditor)
                    .WithPosition("3")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false
                    }))
                .WithField("Cookies", field => field
                    .OfType(nameof(ValuesField))
                    .WithDisplayName("Cookies")
                    .WithPosition("4")
                    .WithSettings(new ValuesFieldSettings
                    {
                        Hint = "Cookies that will be added once user has accepted cookie policy.",
                        NewItemPlaceholder = "Enter cookie names"
                    }))
                .WithField("Accept", field => field
                    .OfType(nameof(CodeField))
                    .WithDisplayName("Accept")
                    .WithPosition("5")
                    .WithSettings(new CodeFieldSettings
                    {
                        Language = "javascript"
                    }))
                .WithField("Revoke", field => field
                    .OfType(nameof(CodeField))
                    .WithDisplayName("Revoke")
                    .WithPosition("6")
                    .WithSettings(new CodeFieldSettings
                    {
                        Language = "javascript"
                    })));

            _contentDefinitionManager.AlterTypeDefinitionAsync(RawCookieContentTypeName, builder => builder
                .MergeSettings(JObject.FromObject(new
                {
                    Category = "Content",
                    Description = "Use CIVIC cookie control for users to configure consent.",
                    Icon = "cookie"
                }))
                .DisplayedAs("Raw Cookie")
                .WithPart(nameof(TitlePart), builder => builder
                    .WithPosition("0"))
                .WithPart(RawCookieContentTypeName, builder => builder
                    .WithPosition("1")));

            return 2;
        }

        public int UpdateFrom2()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(Models.CivicCookieControl), builder => builder
                .WithField(nameof(Models.CivicCookieControl.AcceptAllButtonLabel), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Accept All Button Label")
                    .WithPosition("14")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Text used within the general, affirmative button for opting into all available options."
                    }))
                .WithField(nameof(Models.CivicCookieControl.RejectAllButtonLabel), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Reject All Button Label")
                    .WithPosition("15")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Text used within the general, affirmative button for opting into all available options."
                    }))
                .WithField(nameof(Models.CivicCookieControl.CloseButtonLabel), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Close Button Label")
                    .WithPosition("16")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "The text used to describe the control button to dismiss Cookie Control. Depending on close style this property may only be read by screen readers."
                    }))
                .WithField(nameof(Models.CivicCookieControl.OnToggleLabel), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("On Toggle Label")
                    .WithPosition("17")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "The descriptive text use on the preference control to indicate the user has consented."
                    }))
                .WithField(nameof(Models.CivicCookieControl.OffToggleLabel), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Off Toggle Label")
                    .WithPosition("18")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "The descriptive text use on the preference control to indicate the user has not consented."
                    }))
                .WithField(nameof(Models.CivicCookieControl.SettingsLabel), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Settings Button Label")
                    .WithPosition("19")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "The text used within the control button to open the main preference panel."
                    }))
                .WithField(nameof(Models.CivicCookieControl.NotifyTitle), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Notify Title")
                    .WithPosition("14")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Heading text used to announce the use of cookies for initial displays such as notify."
                    }))
                 .WithField(nameof(Models.CivicCookieControl.NotifyDescription), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Notify Description")
                    .WithEditor(TextAreaEditor)
                    .WithPosition("15")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = false,
                        Hint = "Main body text used to describe the use of cookies for initial displays such as notify."
                    })));

            return 3;
        }

        #endregion
    }
}
