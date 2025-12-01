-- 1. USER tablosunu oluşturma (Güncellenmiş)
CREATE TABLE [User] (
    -- Kullanıcı kimliği, otomatik artan birincil anahtar
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    -- Kullanıcı adı, boş bırakılamaz ve benzersiz olmalı
    Username NVARCHAR(50) NOT NULL, 
    -- Şifre, boş bırakılamaz
    Password NVARCHAR(100) NOT NULL, 
    -- E-posta adresi
    Email NVARCHAR(100) NULL 
);

---

-- 2. TaskItem tablosunu oluşturma (Güncellenmiş)
CREATE TABLE TaskItem (
    -- Görev kimliği, otomatik artan birincil anahtar
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    -- Görev başlığı, boş bırakılamaz
    Title NVARCHAR(200) NOT NULL,
    -- Görev açıklaması
    Description NVARCHAR(MAX) NULL,
    -- Görevin tamamlanıp tamamlanmadığını belirten boolean (bit) alan
    IsCompleted BIT NOT NULL DEFAULT 0,
    -- Görevin oluşturulma zamanı, varsayılan olarak mevcut zaman damgası
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    -- Görevi oluşturan kullanıcıya referans veren yabancı anahtar (Artık INT)
    UserId INT NOT NULL,

    -- Yabancı Anahtar Kısıtlaması (Foreign Key Constraint)
    CONSTRAINT FK_TaskItem_User FOREIGN KEY (UserId)
    -- [User] tablosundaki yeni Id alanına referans veriyor
    REFERENCES [User](Id)
    -- Kullanıcı silindiğinde ilgili görevlerin ne yapılacağını belirtebiliriz (örneğin, CASCADE, NO ACTION, SET NULL)
    -- ON DELETE CASCADE -- Örnek: Kullanıcı silinirse görevleri de siler
);