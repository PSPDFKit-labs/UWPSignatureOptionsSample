﻿using PSPDFKit.Document;
using PSPDFKit.Pdf;
using PSPDFKit.Pdf.ElectronicSignatures;
using PSPDFKit.UI;
using PSPDFKit.UI.ToolbarComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSignatureOptionsSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OnPDFViewInitialized(PdfView sender, Document args)
        {
            var document = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/demo.pdf"));
            var toolbarItems = PDFView.GetToolbarItems();
            var signToolIndex = toolbarItems.IndexOf(toolbarItems.First(item => item is SignatureToolbarItem));
            toolbarItems.RemoveAt(signToolIndex);
            await PDFView.SetToolbarItemsAsync(toolbarItems);

            await PDFView.Controller.ShowDocumentWithViewStateAsync(DocumentSource.CreateFromStorageFile(document), new ViewState
            {
                // Note that electronic signature options can only be set before the document is loaded.
                ElectronicSignatureOptions = new SignatureOptions()
                {
                    CreationModes = new[]
                   {
                       ElectronicSignatureCreationMode.Type,
                       ElectronicSignatureCreationMode.Draw,
                   }
                }
            });
        }

        private async void OnOpenSignatureUIClicked(object sender, RoutedEventArgs e)
        {
            await PDFView.Controller.SetViewStateAsync(new ViewState
            {
                InteractionMode = InteractionMode.InkSignature
            });
        }
    }
}
