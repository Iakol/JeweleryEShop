using Jewelery.data;
using Jewelery.Models.Product_model;

using Jewelery.Infrastructure;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Servise.ImageService;
using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Jewelery.Servise.DetermineTheSizeService
{
    public class DetermineTheSizeService : IDetermineTheSizeService
    {
        private readonly IImageService _imageService;
        private readonly AppDBContext _db;

        public DetermineTheSizeService(IImageService imageService, AppDBContext db) 
        {
            _imageService = imageService;
            _db = db;
        }
        public void DeleteDetermineTheSizePage(int id)
        {
            DetermineTheSize pagetoDelete = _db.DetermineTheSizes.Include(p => p.determineTheSizeObjects).FirstOrDefault(p => p.DetermineTheSize_Id == id);
            DeleteDetermineSizeObject(pagetoDelete.determineTheSizeObjects.ToList());
            _db.Localizations.Remove(_db.Localizations.Find(pagetoDelete.Tittle));
            _db.DetermineTheSizes.Remove(pagetoDelete);
            _db.SaveChanges();
        }

        public List<DetermineSizeSelectList> GetDetermineSizeSelectList()
        {
            List<DetermineSizeSelectList> list = _db.DetermineTheSizes.Select(o => new DetermineSizeSelectList 
            {
                DetermineSize_Id = o.DetermineTheSize_Id,
                Name = o.Name,
            }).ToList();
            return list;
        }

        public DetermineTheSizePageDTO GetDetermineTheSizePage(int id)
        {
            DetermineTheSizePageDTO page = _db.DetermineTheSizes.Select(d => new DetermineTheSizePageDTO 
            {
                id = d.DetermineTheSize_Id,
                name = d.Name,
                TittleUKR = _db.Localizations.FirstOrDefault(l=>l.Value_Id == d.Tittle).UKR,
                TittleENG = _db.Localizations.FirstOrDefault(l => l.Value_Id == d.Tittle).ENG,
                Page = new List<DetermineTheSizePageObjectDTO>(),
            }).FirstOrDefault(p => p.id == id);
            page.Page = GetPageObjectListByPageId(id).ToList();
            return page;
        }

        public IQueryable<DetermineTheSizePageObjectDTO> GetPageObjectListByPageId(int id) 
        {
            IQueryable<DetermineTheSizePageObjectDTO>  list = _db.DetermineTheSizeObjects.Where(o => o.DetermineTheSize_Id == id).Select(o => new DetermineTheSizePageObjectDTO 
            {
                ObjectId = o.DetermineTheSizeObject_Id,
                placeInOrder = o.placeInOrder,
                valueUKR = _db.Localizations.FirstOrDefault(l => l.Value_Id == o.Value).UKR,
                valueENG = _db.Localizations.FirstOrDefault(l => l.Value_Id == o.Value).ENG,
                type = o.Type
            }).OrderBy(o => o.placeInOrder);
            return list;
        }



        public List<DetermineTheSizePageDTO> GetDetermineTheSizePageList()
        {
            List<DetermineTheSizePageDTO> list = _db.DetermineTheSizes.Select(d => new DetermineTheSizePageDTO
            {
                id = d.DetermineTheSize_Id,
                name = d.Name,
                TittleUKR = _db.Localizations.FirstOrDefault(l => l.Value_Id == d.Tittle).UKR,
                TittleENG = _db.Localizations.FirstOrDefault(l => l.Value_Id == d.Tittle).ENG,
                Page = new List<DetermineTheSizePageObjectDTO>()
            }).ToList();
            foreach (var item in list) 
            {
                item.Page = GetPageObjectListByPageId(item.id).ToList();
            }
            return list;
        }

        public void SetUpDetermineTheSize(DetermineTheSizePageObjectFormDTO obj)
        {
            DetermineTheSize page = new DetermineTheSize();
            if (obj.id != 0)
            {
                page = _db.DetermineTheSizes.Include(d => d.determineTheSizeObjects).FirstOrDefault(d => d.DetermineTheSize_Id == obj.id);

                if (page.determineTheSizeObjects.Count() > 0)
                {
                    var listTodelete = page.determineTheSizeObjects
                    .Where(o =>
                    (obj.Text == null || !obj.Text.Any(t => t.ObjectId == o.DetermineTheSizeObject_Id)) &&
                    (obj.Photos == null || !obj.Photos.Any(t => t.ObjectId == o.DetermineTheSizeObject_Id))
                    ).ToList();
                    DeleteDetermineSizeObject(listTodelete);
                }
                LocalizationModel TittleLocal = _db.Localizations.FirstOrDefault(l => l.Value_Id == page.Tittle);
                TittleLocal.UKR = obj.TittleUkr;
                TittleLocal.ENG = obj.TittleEng;
                page.Name = obj.Name;
                _db.DetermineTheSizes.Update(page);
                _db.Localizations.Update(TittleLocal);
                _db.SaveChanges();

            }
            else
            {
                LocalizationModel TitleLocal = new LocalizationModel
                {
                    Type_Variable = 15,
                    ENG = obj.TittleEng,
                    UKR = obj.TittleUkr
                };
                _db.Localizations.Add(TitleLocal);
                _db.SaveChanges();

                page.Name = obj.Name;
                page.Tittle = TitleLocal.Value_Id;
                _db.DetermineTheSizes.Add(page);
                _db.SaveChanges();
            }

            if (obj.Photos != null && obj.Photos.Count() > 0) { 
                foreach (var item in obj.Photos)
                {

                    if (item.ObjectId == 0)
                    {
                        if (((item.imageUKR != null && item.imageENG == null) || (item.imageUKR == null && item.imageENG != null)) && item.BothSame)
                        {
                            if (item.imageUKR != null)
                            {
                                item.imageENG = item.imageUKR;
                            }
                            else if (item.imageENG != null)
                            {
                                item.imageUKR = item.imageENG;
                            }
                            else
                            {
                                throw new J_BadRequestExeption("Both image is null");
                            }
                        }

                        LocalizationModel photoLocal = new LocalizationModel();
                        photoLocal.Type_Variable = 13;
                        photoLocal.UKR = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageUKR);
                        photoLocal.ENG = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageENG);


                        _db.Localizations.Add(photoLocal);
                        _db.SaveChanges();


                        DetermineTheSizeObject photo = new DetermineTheSizeObject
                        {
                            DetermineTheSize_Id = page.DetermineTheSize_Id,
                            Value = photoLocal.Value_Id,
                            placeInOrder = item.placeInOrder,
                            Type = true
                        };

                        _db.DetermineTheSizeObjects.Add(photo);

                    }
                    else if (item.ObjectId != 0)
                    {
                        DetermineTheSizeObject photo = _db.DetermineTheSizeObjects.FirstOrDefault(p => p.DetermineTheSizeObject_Id == item.ObjectId);
                        if (photo != null)
                        {
                            LocalizationModel photoLocal = _db.Localizations.FirstOrDefault(l => l.Value_Id == photo.Value);
                            if (item.imageUKR == null && item.ChangeUKR == "Delete" && item.BothSame && (item.imageENG != null || item.ChangeENG == null))
                            {
                                if (item.imageENG != null)
                                {
                                    _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.ENG);
                                    _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.UKR);
                                    photoLocal.ENG = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageENG);
                                    photoLocal.UKR = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageENG);

                                }
                                else if (item.ChangeENG == null)
                                {
                                    _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.UKR);
                                    photoLocal.UKR = _imageService.DetermineSizeCopyImage(photoLocal.ENG);
                                }
                                else
                                {
                                    throw new J_BadRequestExeption("Doubt Condition");
                                }
                            }
                            if (item.imageENG == null && item.ChangeENG == "Delete" && item.BothSame && (item.imageUKR != null || item.ChangeUKR == null))
                            {
                                if (item.imageUKR != null)
                                {
                                    _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.ENG);
                                    _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.UKR);
                                    photoLocal.ENG = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageUKR);
                                    photoLocal.UKR = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageUKR);

                                }
                                else if (item.imageUKR == null)
                                {
                                    _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.ENG);
                                    photoLocal.ENG = _imageService.DetermineSizeCopyImage(photoLocal.UKR);
                                }
                                else
                                {
                                    throw new J_BadRequestExeption("Doubt Condition");
                                }

                            }

                            if (item.imageUKR != null && item.ChangeUKR == "Change")
                            {
                                _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.UKR);
                                photoLocal.UKR = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageUKR);

                            }
                            if (item.imageENG != null && item.ChangeENG == "Change")
                            {
                                _imageService.DeleteDetermineSizeObjectPhotoImage(photoLocal.ENG);
                                photoLocal.UKR = _imageService.CreateDetermineSizeObjectPhotoImage(item.imageENG);

                            }

                            photo.placeInOrder = item.placeInOrder;
                            _db.Localizations.Update(photoLocal);
                            _db.DetermineTheSizeObjects.Update(photo);


                        }
                        else
                        {
                            throw new J_NotFoundExeption("Object not Found");

                        }
                    }
                    else
                    {
                        throw new J_BadRequestExeption("Bad Determize Size Object");

                    }
                }
            }

            if (obj.Text != null && obj.Text.Count() > 0)
            {
                foreach (var item in obj.Text)
                {
                    if (item.ObjectId == 0)
                    {

                        LocalizationModel TextLocal = new LocalizationModel();
                        TextLocal.Type_Variable = 14;
                        TextLocal.UKR = item.textUKR;
                        TextLocal.ENG = item.textENG;


                        _db.Localizations.Add(TextLocal);
                        _db.SaveChanges();


                        DetermineTheSizeObject text = new DetermineTheSizeObject
                        {
                            DetermineTheSize_Id = page.DetermineTheSize_Id,
                            Value = TextLocal.Value_Id,
                            placeInOrder = item.placeInOrder,
                            Type = false
                        };

                        _db.DetermineTheSizeObjects.Add(text);

                    }
                    else if (item.ObjectId != 0)
                    {
                        DetermineTheSizeObject text = _db.DetermineTheSizeObjects.FirstOrDefault(p => p.DetermineTheSizeObject_Id == item.ObjectId);
                        if (text != null)
                        {
                            LocalizationModel textLocal = _db.Localizations.FirstOrDefault(l => l.Value_Id == text.Value);
                            textLocal.ENG = item.textENG;
                            textLocal.UKR = item.textUKR;
                            text.placeInOrder = item.placeInOrder;
                            _db.Localizations.Update(textLocal);
                            _db.DetermineTheSizeObjects.Update(text);

                        }
                        else
                        {
                            throw new J_NotFoundExeption("Object not Found");

                        }
                    }
                    else
                    {
                        throw new J_BadRequestExeption("Bad Determize Size Object");

                    }

                }
            }
           
            
            
            _db.SaveChanges();



        }

        public void DeleteDetermineSizeObject(List<DetermineTheSizeObject> objects) 
        {
            foreach (var item in objects)
            {
                LocalizationModel objectLocal = _db.Localizations.FirstOrDefault(p => p.Value_Id == item.Value);
                if (item.Type == true)
                {
                    _imageService.DeleteDetermineSizeObjectPhotoImage(objectLocal.UKR);
                    _imageService.DeleteDetermineSizeObjectPhotoImage(objectLocal.ENG);
                }

                _db.DetermineTheSizeObjects.Remove(item);
                _db.Localizations.Remove(objectLocal);
            }
            _db.SaveChanges();

        }
        public void DeleteDetermineSizeObject(DetermineTheSizeObject obj) 
        {
            LocalizationModel objectLocal = _db.Localizations.FirstOrDefault(p => p.Value_Id == obj.Value);
            if (obj.Type == true)
            {
                _imageService.DeleteDetermineSizeObjectPhotoImage(objectLocal.UKR);
                _imageService.DeleteDetermineSizeObjectPhotoImage(objectLocal.ENG);
            }

            _db.DetermineTheSizeObjects.Remove(obj);
            _db.Localizations.Remove(objectLocal);
            _db.SaveChanges();
        }

    }
}
