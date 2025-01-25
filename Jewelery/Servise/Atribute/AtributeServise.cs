using Jewelery.data;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Jewelery.Servise.ProductServise
{
    public class AtributeServise : IAtributeServise
    {
        private readonly AppDBContext _db;
        private readonly IOptionServise _optionServise;

        public AtributeServise(AppDBContext db, IOptionServise optionServise)
        {
            _db = db;
            _optionServise = optionServise;
        }

        public void AddAtribute(AtributeCMSDTO entiry)
        {
            SqlParameter Atribute_name_UKR = new SqlParameter("@Atribute_name_UKR", entiry.Atribute_name_UKR);
            SqlParameter Atribute_name_ENG = new SqlParameter("@Atribute_name_ENG", entiry.Atribute_name_ENG);
            SqlParameter Product_id = new SqlParameter("@Product_id", entiry.Product_id);


            SqlParameter Unit = new SqlParameter("@Unit", DBNull.Value);
            if (entiry.Unit != null)
            {
                Unit = new SqlParameter("@Unit", entiry.Unit);

            }

            SqlParameter DetermineTheSize_Id = new SqlParameter("@DetermineTheSize_Id",DBNull.Value);


            if (entiry.DetermineTheSize_Id != null) 
            {
                DetermineTheSize_Id = new SqlParameter("@DetermineTheSize_Id", entiry.DetermineTheSize_Id);

            }

            SqlParameter OUT_Id = new SqlParameter
            {
                ParameterName = "@OUT_Id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output

            };

            _db.Database.ExecuteSqlRaw("EXECUTE AddAtribute @Atribute_name_UKR,@Atribute_name_ENG,@Product_id,@Unit,@DetermineTheSize_Id,@OUT_Id OUTPUT",
                Atribute_name_UKR, Atribute_name_ENG, Product_id, Unit, DetermineTheSize_Id, OUT_Id);

            foreach (var item in entiry.Options) 
            
            {
                item.Atribute_id = (int)OUT_Id.Value;
                _optionServise.AddOption(item);


            }
        }

        public void DeleteAtribute(int Atribute_id)
        {
            if (_db.Atributes.FirstOrDefault(a => a.Atribute_id == Atribute_id) != null)
            {
                SqlParameter Atribute_Id = new SqlParameter("@Atribute_id", Atribute_id);
                _db.Database.ExecuteSqlRaw("EXECUTE DeleteAtribute @Atribute_Id",
                    Atribute_Id);

                _optionServise.RemoveOptionByAtributeId(Atribute_id);
            }
            else 
            {
                throw new J_NotFoundExeption();
            }

        }

        public List<AtributeDTO> GetAtributeByProduct(int Product_id, int lang)
        {
            if (_db.Products.FirstOrDefault(p => p.Product_id == Product_id) != null) 
            {
                SqlParameter Product_idPar = new SqlParameter("@Product_id", Product_id);
                SqlParameter Language = new SqlParameter("@Language", lang);

                List<AtributeDTO> Listitem = _db.Set<AtributeDTO>().FromSqlRaw("EXECUTE GetAtributeByProduct @Product_id, @Language",
                    Product_idPar, Language).ToList();

                foreach (AtributeDTO item in Listitem) 
                {
                    item.Options = _optionServise.GetOptionByAtribute(item.Atribute_id);

                }

                return Listitem;

            }else { throw new J_NotFoundExeption(); }
        }

        public List<AtributeCMSDTO> GetAtributeByProductCMS(int Product_id)
        {
            if (_db.Products.FirstOrDefault(p => p.Product_id == Product_id) != null)
            {
                SqlParameter Product_idPar = new SqlParameter("@Product_id", Product_id);

                List<AtributeCMSDTO> Listitem = _db.Set<AtributeCMSDTO>().FromSqlRaw("EXECUTE GetAtributeByProductCMS @Product_id",
                    Product_idPar).ToList();

                foreach (AtributeCMSDTO item in Listitem)
                {
                    item.Options = _optionServise.GetOptionByAtribute(item.Atribute_id);

                }

                return Listitem;

            }
            else { throw new J_NotFoundExeption(); }
        }


        public void UpdateAtribute(AtributeCMSDTO entiry)
        {
            SqlParameter Atribute_id = new SqlParameter("@Atribute_id", entiry.Atribute_id);
            SqlParameter Atribute_name_UKR = new SqlParameter("@Atribute_name_UKR", entiry.Atribute_name_UKR);
            SqlParameter Atribute_name_ENG = new SqlParameter("@Atribute_name_ENG", entiry.Atribute_name_ENG);
            SqlParameter Product_id = new SqlParameter("@Product_id", entiry.Product_id);
            SqlParameter Unit = new SqlParameter("@Unit", DBNull.Value);
            if (entiry.Unit != null)
            {
                Unit = new SqlParameter("@Unit", entiry.Unit);

            }

            SqlParameter DetermineTheSize_Id = new SqlParameter("@DetermineTheSize_Id", DBNull.Value);


            if (entiry.DetermineTheSize_Id != null)
            {
                DetermineTheSize_Id = new SqlParameter("@DetermineTheSize_Id", entiry.DetermineTheSize_Id);

            }

            _db.Database.ExecuteSqlRaw("EXECUTE UpdateAtribute @Atribute_id, @Atribute_name_UKR,@Atribute_name_ENG,@Product_id,@Unit,@DetermineTheSize_Id",
                Atribute_id, Atribute_name_UKR, Atribute_name_ENG, Product_id, Unit, DetermineTheSize_Id);

            List<Option> OldOption = _db.Options
            .Where(o => o.Atribute_id == entiry.Atribute_id)
            .ToList();

            foreach (var item in OldOption)
            {
                if (!entiry.Options.Any(o => o.Option_id == item.Option_id))
                {
                    _optionServise.RemoveOption(item.Option_id);
                }
            }

            if (entiry.Options != null)
            {
                foreach (var item in entiry.Options)
                {
                    if (item.Option_id != 0)
                    {

                        _optionServise.UpdateOprion(item);
                    }
                    else
                    {
                        _optionServise.AddOption(item);
                    }
                }
            }

            
        }
    }
}
