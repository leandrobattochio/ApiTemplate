using System;
using Template.Core;

namespace Template.Domain.Models
{
    /// <summary>
    /// Entidade dos livros.
    /// </summary>
    public class Livro : NotificationEntity
    {
        /// <summary>
        /// Construtor da entidade "Livro".
        /// </summary>
        /// <param name="imagemCapa"></param>
        /// <param name="titulo"></param>
        /// <param name="iSBN"></param>
        /// <param name="editora"></param>
        /// <param name="autor"></param>
        /// <param name="sinopse"></param>
        /// <param name="dataPublicacao"></param>
        public Livro(string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse, DateTime dataPublicacao)
        {
            ImagemCapa = imagemCapa;
            Titulo = titulo;
            ISBN = iSBN;
            Editora = editora;
            Autor = autor;
            Sinopse = sinopse;
            DataPublicacao = dataPublicacao;
        }

        public Livro(Guid id, string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse,
            DateTime dataPublicacao) : this(imagemCapa, titulo, iSBN, editora, autor, sinopse, dataPublicacao)
        {
            Id = id;
        }

        protected Livro()
        {

        }


        // Dominios ricos: setter privado.
        // Qualquer alteração deve ser feita através de métodos
        // da propria entidade.
        public string ImagemCapa { get; private set; }
        public string Titulo { get; private set; }
        public string ISBN { get; private set; }
        public string Editora { get; private set; }
        public string Autor { get; private set; }
        public string Sinopse { get; private set; }
        public DateTime DataPublicacao { get; private set; }
    }
}