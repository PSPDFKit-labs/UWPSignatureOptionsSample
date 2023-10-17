# Updating Signature Options and Interaction Mode programatically

The signature options and interaction mode can be updated programatically by calling the `ShowDocumentWithViewStateAsync` method on the `Controller` instance.

```csharp
        private async void OnPDFViewInitialized(PdfView sender, Document args)
        {
            var document = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/demo.pdf"));
            await PDFView.Controller.ShowDocumentWithViewStateAsync(DocumentSource.CreateFromStorageFile(document), new ViewState
            {
                ElectronicSignatureOptions = new SignatureOptions()
                {
                    CreationModes = new[]
                   {
                       ElectronicSignatureCreationMode.Type,
                       ElectronicSignatureCreationMode.Draw,
                   }
                },
                InteractionMode = InteractionMode.InkSignature
            });
        }
```

> [!NOTE]  
> 1. Controller instance is only available after the PDFView is Initialized.
> 2. `ElectronicSignatureOptions` can only be set before a document is loaded.
