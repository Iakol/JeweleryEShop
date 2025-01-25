

let imageCount = 0;
function AddImageField(event)
{

    console.log(event.target);

    const Imginput = document.createElement('input');
    Imginput.type = "file";
    Imginput.accept = "image/*";


    Imginput.addEventListener('change', function (event) { 
        
        let lastIndex;
        if (event.target.parentElement)
        {
            lastIndex = parseInt(event.target.parentElement.dataset.LastIndex, 10);
        }



        const container = document.querySelector('.Image_Container');

        Imginput.dataset.LastPreIndex = imageCount;

        var Imageindex = parseInt(container.dataset.imagecounter, 10);

        console.log(Imageindex);
        const index = Imageindex;

        Imageindex = Imageindex + 1;
        container.dataset.imagecounter = Imageindex;

        Imginput.dataset.LastIndex = index;


        Imginput.name = "Images[" + index + "].ImageFile";
        Imginput.id = "Images[" + index + "].ImageFile";




        const ImageGroup = document.createElement('div');
        ImageGroup.className = "Image_group";
        ImageGroup.id = "Image_group_" + index;

        const PreviewImgContainer = document.createElement('div');
        PreviewImgContainer.classList.add("PreviewContainerForImage");

        const PreviewImg = document.createElement("img");
        PreviewImg.src = "#";
        PreviewImg.alt = "Image Preview " + index;
        PreviewImg.id = "ImagePreview_" + index;

        const ImginputContainer = document.createElement('div');
        ImginputContainer.dataset.LastIndex = index;

        const containerModal = document.createElement('div');

        //

        containerModal.classList.add("ImageModal");
        containerModal.dataset.imagemodalfor = index;
        containerModal.onclick = function (event) {
            if (event.target === this) { 
                OpenModal(index);
            }
        };


        //

        const containerImagePreviewModal = document.createElement('div');
        containerImagePreviewModal.classList.add("ImageModalContainer");

        //------//
        const PreviewImgModal = document.createElement("img");
        PreviewImgModal.src = "#";
        PreviewImgModal.alt = "Image Preview @i " + index;
        PreviewImgModal.id = "ImageModalPreview_" + index;
        PreviewImgModal.classList.add("ModalImagePreview");

        //------//

        //

        const containerInputs = document.createElement('div');
        containerInputs.classList.add("InputsModalContainer");

        //

        //------//
        const AltTextImageContainer = document.createElement('div');
        AltTextImageContainer.classList.add("AltTextImageContainer");

        const AltTextLabel = document.createElement('label');
        AltTextLabel.htmlFor = "Images[" + index + "].Alt_text";
        AltTextLabel.textContent = "Alt Text";

        const AltTextInput = document.createElement('input');
        AltTextInput.type = "text";
        AltTextInput.name = "Images[" + index + "].Alt_text";
        AltTextInput.id = "Images[" + index + "].Alt_text";
        //
        const ChooseNewInputImageContainer = document.createElement('div');
        ChooseNewInputImageContainer.dataset.LastIndex = index;
        ChooseNewInputImageContainer.classList.add("ChooseNewInputImageContainer");


         //

        

        containerImagePreviewModal.appendChild(PreviewImgModal);
        AltTextImageContainer.appendChild(AltTextLabel);
        AltTextImageContainer.appendChild(AltTextInput);
        containerInputs.appendChild(AltTextImageContainer);
        containerInputs.appendChild(ChooseNewInputImageContainer);

        containerModal.appendChild(containerImagePreviewModal);
        containerModal.appendChild(containerInputs);




        //------//



        

        const RemoveButtonContainer = document.createElement('div');

        RemoveButtonContainer.classList.add("Buttons");

        const RemoveButton = document.createElement('button');

        RemoveButton.type = "button";

        RemoveButton.onclick = function () {
            RemoveImage(index);
        };
        RemoveButton.textContent = "" // Переробити щоб з куки витягувалося мова
        RemoveButton.classList.add("RemoveImageButton")

        const RemoveButtonIcon = document.createElement('i');
        RemoveButtonIcon.classList.add("bi");
        RemoveButtonIcon.classList.add("bi-trash3");
        RemoveButton.appendChild(RemoveButtonIcon);
        //

        const ModalButton = document.createElement('button');

        ModalButton.type = "button";

        ModalButton.onclick = function () {
            OpenModal(index);//change To open Modal
        };
        ModalButton.textContent = "" // Переробити щоб з куки витягувалося мова
        ModalButton.classList.add("OpenImageButton")

        const ModalButtonIcon = document.createElement('i');
        ModalButtonIcon.classList.add("bi");
        ModalButtonIcon.classList.add("bi-search");
        ModalButton.appendChild(ModalButtonIcon);
           
        //


        container.appendChild(ImageGroup);

        ImageGroup.appendChild(PreviewImgContainer);
        //ImageGroup.appendChild(ImginputContainer);
        //ImageGroup.appendChild(RemoveButtonContainer);
        ImageGroup.appendChild(containerModal);


        PreviewImgContainer.appendChild(PreviewImg);
        PreviewImgContainer.appendChild(RemoveButtonContainer);


        //ImginputContainer.appendChild(Imginput);
        ChooseNewInputImageContainer.appendChild(Imginput);


        RemoveButtonContainer.appendChild(RemoveButton);
        RemoveButtonContainer.appendChild(ModalButton); 

        

        if (lastIndex !== undefined || lastIndex !== null)
        {
            RemoveImage(lastIndex);
        }
        
        PreviewImage(event, index);

            
        

    });

    Imginput.click();
    

    
}

function PreviewImage(event, index)
{
   var reader = new FileReader();
   reader.onload = function ()
   {
       var output = document.querySelector("#ImagePreview_" + index);
       var outputModal = document.querySelector("#ImageModalPreview_" + index);

       output.src = reader.result;
       output.style.display = 'block';
       outputModal.src = reader.result;
       outputModal.style.display = 'block';
   }

        reader.readAsDataURL(event.target.files[0]);

}


function RemoveImage(index)
{
    const ImageGroupeToRemove = document.querySelector("#Image_group_" + index);
    if (ImageGroupeToRemove)
    {
        ImageGroupeToRemove.remove();
    }

}

function OpenModal(index) {
    const OpenModal = document.querySelector(`.ImageModal[data-imagemodalfor="${index}"]`);
    OpenModal.classList.toggle("OpenModal");

}
