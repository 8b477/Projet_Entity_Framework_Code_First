# [EN](README_EN.md) / [FR](README.md)

---------------------------------
# 1.
![step one](Screen_EF/create_project_step1.png)

# 2.
![step one](Screen_EF/create_project_step2.png)

# 3.
![step one](Screen_EF/create_project_step3.png)

# 4.
![step one](Screen_EF/create_project_step4.png)


---------------------------------

## CLI (Ligne de commande)

*Via le terminal (clic droit sur le projet et choisir => 'Ouvrir dans terminal'.*  

----------------

### NuGet Package à installer
**A faire en début de projet**  

`dotnet add package Microsoft.EntityFrameworkCore.Design`  
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

### CLI
**A faire à la fin quand la structure des fichiers a été établie**  

1. `dotnet ef migrations add init`
2. `dotnet ef database update`

-----------------------
# Shéma à suivre pour la création de A à Z *(simple)*.

## 1. **Créer son ou ses modèles.**

   *C'est quoi un modèle ?*  
   
   *=> C'est tout simplement la représentation de nos tables en base de données.*

```C#
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
```

---------------------------------

## 2. **Créer la classe du contexte.**

```C#
 public partial class MyContext : DbContext
    {
                
        public MyContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {}
    }
```


## Ajouter le(s) modèle(s) créée au point 1.

```C#
    public partial class MyContext : DbContext
    {        
        public MyContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
        protected override void OnModelCreating(ModelBuilder modelBuilder){ }

        public DbSet<Blog> Blog { get; set; }
    }
```

---------------------------------

## 3. **En même temp rajouter la chaîne de connection dans le fichier program.cs :**

```C#
builder.Services.AddDbContext<MyContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("nom_référence_json"));
});
```
   *- Celui-ci vas rechercher la chaîne de connection dans le fichier appsettings.json  
    => "nom_référence_json", voire étape suivante*

---------------------------------

## 4. **Le fichier .json ce présente comme ceci :**
Aide pour la syntaxe de la connection string : https://www.connectionstrings.com/
```json
"ConnectionStrings": {
    "nom_référence_json": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nom_De_Ma_DB;Integrated Security=True;"
  }
```

---------------------------------

## 5. **Créer une classe [NomDuModel]EntityTypeConfiguration.cs (si besoin).**  
  *=> Le but ici est de ne pas trop charger la classe de contexte de base.*   

```C#
 public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
        {
            public void Configure(EntityTypeBuilder<Blog> builder)
            {
                builder
                    .Property(b => b.Url)
                    .IsRequired();
            }
        }
```

---------------------------------

### Et ajouter celle-ci dans vôtre classe de contexte, comme ci dessous

```C#
    public partial class MyContext : DbContext
    {
                
        public MyContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
        }

        public DbSet<Blog> Blog { get; set; }
    }
```

![](Screen_EF/ExempleAjoutDeNotreClassDeContrainte.png)
----------------------
  
## Conclusion
Il suffit ensuite d'exécuter les commandes de 'migrations' puis de 'update' et la base de donnée sera créée ainsi que les tables et différentes contraintes.
