"use strict";

var imageCount = 0;
function AddImageField(event) {

        console.log(event.target);

        var Imginput = document.createElement('input');
        Imginput.type = "file";
        Imginput.accept = "image/*";

        Imginput.addEventListener('change', function (event) {

                var lastIndex = undefined;
                if (event.target.parentElement) {
                        lastIndex = parseInt(event.target.parentElement.dataset.LastIndex, 10);
                }

                var container = document.querySelector('.Image_Container');

                Imginput.dataset.LastPreIndex = imageCount;

                var Imageindex = parseInt(container.dataset.imagecounter, 10);

                console.log(Imageindex);
                var index = Imageindex;

                Imageindex = Imageindex + 1;
                container.dataset.imagecounter = Imageindex;

                Imginput.dataset.LastIndex = index;

                Imginput.name = "Images[" + index + "].ImageFile";
                Imginput.id = "Images[" + index + "].ImageFile";

                var ImageGroup = document.createElement('div');
                ImageGroup.className = "Image_group";
                ImageGroup.id = "Image_group_" + index;

                var PreviewImgContainer = document.createElement('div');
                PreviewImgContainer.classList.add("PreviewContainerForImage");

                var PreviewImg = document.createElement("img");
                PreviewImg.src = "#";
                PreviewImg.alt = "Image Preview " + index;
                PreviewImg.id = "ImagePreview_" + index;

                var ImginputContainer = document.createElement('div');
                ImginputContainer.dataset.LastIndex = index;

                var containerModal = document.createElement('div');

                //

                containerModal.classList.add("ImageModal");
                containerModal.dataset.imagemodalfor = index;
                containerModal.onclick = function (event) {
                        if (event.target === this) {
                                OpenModal(index);
                        }
                };

                //

                var containerImagePreviewModal = document.createElement('div');
                containerImagePreviewModal.classList.add("ImageModalContainer");

                //------//
                var PreviewImgModal = document.createElement("img");
                PreviewImgModal.src = "#";
                PreviewImgModal.alt = "Image Preview @i " + index;
                PreviewImgModal.id = "ImageModalPreview_" + index;
                PreviewImgModal.classList.add("ModalImagePreview");

                //------//

                //

                var containerInputs = document.createElement('div');
                containerInputs.classList.add("InputsModalContainer");

                //

                //------//
                var AltTextImageContainer = document.createElement('div');
                AltTextImageContainer.classList.add("AltTextImageContainer");

                var AltTextLabel = document.createElement('label');
                AltTextLabel.htmlFor = "Images[" + index + "].Alt_text";
                AltTextLabel.textContent = "Alt Text";

                var AltTextInput = document.createElement('input');
                AltTextInput.type = "text";
                AltTextInput.name = "Images[" + index + "].Alt_text";
                AltTextInput.id = "Images[" + index + "].Alt_text";
                //
                var ChooseNewInputImageContainer = document.createElement('div');
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

                var RemoveButtonContainer = document.createElement('div');

                RemoveButtonContainer.classList.add("Buttons");

                var RemoveButton = document.createElement('button');

                RemoveButton.type = "button";

                RemoveButton.onclick = function () {
                        RemoveImage(index);
                };
                RemoveButton.textContent = ""; // Переробити щоб з куки витягувалося мова
                RemoveButton.classList.add("RemoveImageButton");

                var RemoveButtonIcon = document.createElement('i');
                RemoveButtonIcon.classList.add("bi");
                RemoveButtonIcon.classList.add("bi-trash3");
                RemoveButton.appendChild(RemoveButtonIcon);
                //

                var ModalButton = document.createElement('button');

                ModalButton.type = "button";

                ModalButton.onclick = function () {
                        OpenModal(index); //change To open Modal
                };
                ModalButton.textContent = ""; // Переробити щоб з куки витягувалося мова
                ModalButton.classList.add("OpenImageButton");

                var ModalButtonIcon = document.createElement('i');
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

                if (lastIndex !== undefined || lastIndex !== null) {
                        RemoveImage(lastIndex);
                }

                PreviewImage(event, index);
        });

        Imginput.click();
}

function PreviewImage(event, index) {
        var reader = new FileReader();
        reader.onload = function () {
                var output = document.querySelector("#ImagePreview_" + index);
                var outputModal = document.querySelector("#ImageModalPreview_" + index);

                output.src = reader.result;
                output.style.display = 'block';
                outputModal.src = reader.result;
                outputModal.style.display = 'block';
        };

        reader.readAsDataURL(event.target.files[0]);
}

function RemoveImage(index) {
        var ImageGroupeToRemove = document.querySelector("#Image_group_" + index);
        if (ImageGroupeToRemove) {
                ImageGroupeToRemove.remove();
        }
}

function OpenModal(index) {
        var OpenModal = document.querySelector(".ImageModal[data-imagemodalfor=\"" + index + "\"]");
        OpenModal.classList.toggle("OpenModal");
}

