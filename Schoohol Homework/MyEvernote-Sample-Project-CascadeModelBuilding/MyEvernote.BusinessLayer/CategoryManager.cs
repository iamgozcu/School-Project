using MyEvernote.BusinessLayer.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {

     //   Bir kategor silindiği zaman ona bağlı olan şeyleride silmemzi lazım bunun için ise delete methodunu eziyoruz
        public override int Delete(Category category)//ManagerBase içindeki delete methodunu override ile eziyoruz
        {
            NoteManager noteManager = new NoteManager();
            LikedManager liked = new LikedManager();
            CommentManager comment = new CommentManager();

            //Kategori ile işkişi notlarını silinmesi gerekiyor
            foreach (Note note in category.Notes.ToList())//notların içinde dönerek onları delete edicez
            {
                //note ile ilişkili like ların silinmesi
                foreach (Liked like in note.Likes.ToList())
                {
                    liked.Delete(like);
                }

                //Notu ile ilişkili commentlerin silinmesi
                foreach (Comment commet in note.Comments.ToList())
                {
                    comment.Delete(commet);
                }

                noteManager.Delete(note);
            }

            return base.Delete(category);
        }

    }
}
