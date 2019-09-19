using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary18._9._19
{
    public class ClassManager
    {
        public List<Food> Foods()
        {
            using (FoodDBEntities foodDBEntities  = new FoodDBEntities())
            {
               
                List<Food> foods = foodDBEntities.Foods.ToList();

                return foods;

            }
        }

        public List<Food> GetFoodBiggerThanCalory(int calory)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                return foodDBEntities.Foods.Where(m => m.Calories > calory).ToList();
            }

        }

        public List<Food> GetFoodsByFilter(int mincalory,int maxcalory,string name,int grade)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                return foodDBEntities.Foods.Where(m => m.Calories > mincalory && m.Calories<maxcalory&& m.Grade>grade
                    && (name == "" || m.Name.ToUpper().Contains(name.ToUpper()))).ToList();
            }

        }

        public void AddToDB(Food food)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {                
                foodDBEntities.Foods.Add(food);

                foodDBEntities.SaveChanges();
            }
        }

        public void UpdateDB(Food food, int id)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                Food result = foodDBEntities.Foods.SingleOrDefault(b => b.ID == id);
                if (result != null)
                {
                    result.Calories = food.Calories;
                    result.Grade = food.Grade;
                    result.Ingridients = food.Ingridients;
                    result.Name = food.Name;
                    foodDBEntities.SaveChanges();
                }

            }
        }
       
        public void RemoveFromDB(int id)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                Food result = foodDBEntities.Foods.SingleOrDefault(b => b.ID == id);
                if(result!=null)
                {
                    foodDBEntities.Foods.Remove(result);
                }

                foodDBEntities.SaveChanges();

            }

        }
    }
}
