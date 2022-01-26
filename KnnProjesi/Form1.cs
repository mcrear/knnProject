using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnnProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // random veya manuel olarak eklenmiş piksel değerlerinin saklandığı liste
        public List<MyListItem> NoktaListesi = new List<MyListItem>();

        // hedef noktasından dışarıya doğru açılırken k değerine ulaşılana kadar rastlanan noktaların saklandığı liste
        public List<SelectedItem> SecilenlerListesi = new List<SelectedItem>();

        // mXm matrisindeki m değerinin karşılık geldiği matris boyutu.
        public int Uzaklik = 30;

        // etrafında tarama yapmaya başlayacağımız pikselin x eksenindeki yeri
        public int HedefX = 0;

        // etrafında tarama yapmaya başlayacağımız pikselin y eksenindeki yeri
        public int HedefY = 0;

        // etrafında tarama yapmaya başlayacağımız piksele en yakın kaç pikseli inceleyeceğimizin belirlenmesi
        public int KDegeri = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            // birden fazla işlemi asenkron şekilde yönetebilmek için kullanılan sistem çağrılarının çakışmasını engelleyen setleme
            Control.CheckForIllegalCrossThreadCalls = false;

            // yeni bir tread oluşturularak sistem açılışında ekranın işlemler tamamlanmadan gelmesinin sağlanması
            Thread thread1 = new Thread(new ThreadStart(DrawMatris));

            // belirtilen tread işleminin başlatılması
            thread1.Start();
        }

        /// <summary>
        /// Belirtilen konumdan Y ekseni üzerinde distance birim kadar uzakta olan X Ekseninin yine (distance X 2 birim) uzunluğunda alanını tarayan method
        /// tarama işlemi için öncelikle hedef noktasından Y eksenine bir dikme çekilecek şekilde hedef doğru parçasının tam orta noktasına sorgu yapılıyor
        /// sonrasında tesbit edilen orta noktaya +/- değerler verilerek orta noktanın bir alt ve bir üst noktaları dönülüyor. Bu işlem distance değeri kadar tekrarlanıyor
        /// </summary>
        /// <param name="x">belirlenen pikselin x ekseninde ki yerini tutan parametredir</param>
        /// <param name="y">belirlenen pikselin y ekseninde ki yerini tutan parametredir</param>
        /// <param name="distance">belirlenen pikselin merkez pikselden ne kadar uzakta olduğunu ve hedef doğru parçasının kaç birimden olusacağını belirten parametredir.</param>
        /// <returns></returns>
        private int CheckForRight(int x, int y, int distance)
        {
            // random yada manuel oluşturulan piksellerin tutulduğu liste içerisinde hedeflenen doğru parçası üzerinde yer alan piksel listesi çekiliyor
            var items = NoktaListesi.Where(item => item.YEkseni == y + distance && item.XEkseni > x - distance && item.XEkseni < x + distance);

            // listedeki piksel ve hedef pikseller arasındaki mesafeyi tutacak değişkene ilk değerinin setlenmesi
            double pointDistance = 0;

            // listeden gelen değer olup olmadığının sorgulanması. eğer değer varsa bu değerlerin tek tek hesaplanarak işleme alınması
            if (items.Count() > 0)
                foreach (var item in items)
                {
                    // pisagor teoremi ile eksenlerin uzunluklarına bağlı olarak hipotenüs değeri olan mesafenin hesaplanması yapılmaktadır.
                    pointDistance = Math.Sqrt(Math.Pow(item.XEkseni - HedefX, 2) + Math.Pow(item.YEkseni - HedefY, 2));

                    // yapılan hesaplamadan sonra ekranın sol tarafında yer alan listeye piksel bilgilerinin eklenmesi yapılıyor. 
                    lbEslesmeler.Items.Add($"checkForRight=> X:{item.XEkseni}, Y:{item.YEkseni}, Grup:{item.Grup}     Fark X:{item.XEkseni - HedefX}, Y{item.YEkseni - HedefY}, Uzaklık:{distance}");

                    // eğer K değeri kadar noktaya henüz ulaşılmamışsa en yakın piksellerin tutulduğu listeye pikselin eklenme işlemi gerçekleştiriliyor
                    if (SecilenlerListesi.Count() <= KDegeri)
                    {
                        SecilenlerListesi.Add(new SelectedItem { Id = SecilenlerListesi.Count() + 1, Distance = pointDistance, Grup = item.Grup, MyListItemId = item.Id });

                        // Bu ekleme işleminden sonra en yakın olan noktaların diğerlerinden ayırt edilebilmesi için pikselin koordinat değerleri ve yeni değeri gönderilerek piksel kırmızı renge döndürülüyor
                        ChangeColor(item.XEkseni, item.YEkseni, Color.Red);
                    }

                }

            // işlem sonucunda ise kaç farklı pikselin belirtilen doğru parçası üzerinde yer aldığı geri dönülüyor.
            return items.Count();
        }

        /// <summary>
        /// Belirtilen konumdan X ekseni üzerinde distance birim kadar uzakta olan Y Ekseninin yine (distance X 2 birim) uzunluğunda alanını tarayan method
        /// tarama işlemi için öncelikle hedef noktasından X eksenine bir dikme çekilecek şekilde hedef doğru parçasının tam orta noktasına sorgu yapılıyor
        /// sonrasında tesbit edilen orta noktaya +/- değerler verilerek orta noktanın bir alt ve bir üst noktaları dönülüyor. Bu işlem distance değeri kadar tekrarlanıyor
        /// </summary>
        /// <param name="x">belirlenen pikselin x ekseninde ki yerini tutan parametredir</param>
        /// <param name="y">belirlenen pikselin y ekseninde ki yerini tutan parametredir</param>
        /// <param name="distance">belirlenen pikselin merkez pikselden ne kadar uzakta olduğunu ve hedef doğru parçasının kaç birimden olusacağını belirten parametredir.</param>
        /// <returns></returns>
        private int CheckForBottom(int x, int y, int distance)
        {
            // random yada manuel oluşturulan piksellerin tutulduğu liste içerisinde hedeflenen doğru parçası üzerinde yer alan piksel listesi çekiliyor
            var items = NoktaListesi.Where(item => item.XEkseni == x + distance && item.YEkseni > x - distance && item.YEkseni < x + distance);

            // listedeki piksel ve hedef pikseller arasındaki mesafeyi tutacak değişkene ilk değerinin setlenmesi
            double pointDistance = 0;

            // listeden gelen değer olup olmadığının sorgulanması. eğer değer varsa bu değerlerin tek tek hesaplanarak işleme alınması
            if (items.Count() > 0)
                foreach (var item in items)
                {
                    // pisagor teoremi ile eksenlerin uzunluklarına bağlı olarak hipotenüs değeri olan mesafenin hesaplanması yapılmaktadır.
                    pointDistance = Math.Sqrt(Math.Pow(item.XEkseni - HedefX, 2) + Math.Pow(item.YEkseni - HedefY, 2));

                    // yapılan hesaplamadan sonra ekranın sol tarafında yer alan listeye piksel bilgilerinin eklenmesi yapılıyor. 
                    lbEslesmeler.Items.Add($"checkForBottom=> X:{item.XEkseni}, Y:{item.YEkseni}, Grup:{item.Grup}     Fark X:{item.XEkseni - HedefX}, Y{item.YEkseni - HedefY}, Uzaklık:{pointDistance}");

                    // eğer K değeri kadar noktaya henüz ulaşılmamışsa en yakın piksellerin tutulduğu listeye pikselin eklenme işlemi gerçekleştiriliyor
                    if (SecilenlerListesi.Count() <= KDegeri)
                    {
                        SecilenlerListesi.Add(new SelectedItem { Id = SecilenlerListesi.Count() + 1, Distance = pointDistance, Grup = item.Grup, MyListItemId = item.Id });

                        // Bu ekleme işleminden sonra en yakın olan noktaların diğerlerinden ayırt edilebilmesi için pikselin koordinat değerleri ve yeni değeri gönderilerek piksel kırmızı renge döndürülüyor
                        ChangeColor(item.XEkseni, item.YEkseni, Color.Red);
                    }
                }

            // işlem sonucunda ise kaç farklı pikselin belirtilen doğru parçası üzerinde yer aldığı geri dönülüyor.
            return items.Count();
        }

        /// <summary>
        /// Belirtilen konumdan Y ekseni üzerinde distance birim kadar uzakta olan X Ekseninin yine (distance X 2 birim) uzunluğunda alanını tarayan method
        /// tarama işlemi için öncelikle hedef noktasından Y eksenine bir dikme çekilecek şekilde hedef doğru parçasının tam orta noktasına sorgu yapılıyor
        /// sonrasında tesbit edilen orta noktaya +/- değerler verilerek orta noktanın bir alt ve bir üst noktaları dönülüyor. Bu işlem distance değeri kadar tekrarlanıyor
        /// </summary>
        /// <param name="x">belirlenen pikselin x ekseninde ki yerini tutan parametredir</param>
        /// <param name="y">belirlenen pikselin y ekseninde ki yerini tutan parametredir</param>
        /// <param name="distance">belirlenen pikselin merkez pikselden ne kadar uzakta olduğunu ve hedef doğru parçasının kaç birimden olusacağını belirten parametredir.</param>
        /// <returns></returns>
        private int CheckForLeft(int x, int y, int distance)
        {
            // random yada manuel oluşturulan piksellerin tutulduğu liste içerisinde hedeflenen doğru parçası üzerinde yer alan piksel listesi çekiliyor
            var items = NoktaListesi.Where(item => item.YEkseni == y - distance && item.XEkseni > x - distance && item.XEkseni < x + distance);

            // listedeki piksel ve hedef pikseller arasındaki mesafeyi tutacak değişkene ilk değerinin setlenmesi
            double pointDistance = 0;

            // listeden gelen değer olup olmadığının sorgulanması. eğer değer varsa bu değerlerin tek tek hesaplanarak işleme alınması
            if (items.Count() > 0)
                foreach (var item in items)
                {
                    // pisagor teoremi ile eksenlerin uzunluklarına bağlı olarak hipotenüs değeri olan mesafenin hesaplanması yapılmaktadır.
                    pointDistance = Math.Sqrt(Math.Pow(item.XEkseni - HedefX, 2) + Math.Pow(item.YEkseni - HedefY, 2));

                    // yapılan hesaplamadan sonra ekranın sol tarafında yer alan listeye piksel bilgilerinin eklenmesi yapılıyor. 
                    lbEslesmeler.Items.Add($"checkForLeft=> X:{item.XEkseni}, Y:{item.YEkseni}, Grup:{item.Grup}     Fark X:{item.XEkseni - HedefX}, Y{item.YEkseni - HedefY}, Uzaklık:{pointDistance}");

                    // eğer K değeri kadar noktaya henüz ulaşılmamışsa en yakın piksellerin tutulduğu listeye pikselin eklenme işlemi gerçekleştiriliyor
                    if (SecilenlerListesi.Count() <= KDegeri)
                    {
                        SecilenlerListesi.Add(new SelectedItem { Id = SecilenlerListesi.Count() + 1, Distance = pointDistance, Grup = item.Grup, MyListItemId = item.Id });

                        // Bu ekleme işleminden sonra en yakın olan noktaların diğerlerinden ayırt edilebilmesi için pikselin koordinat değerleri ve yeni değeri gönderilerek piksel kırmızı renge döndürülüyor
                        ChangeColor(item.XEkseni, item.YEkseni, Color.Red);
                    }
                }

            // işlem sonucunda ise kaç farklı pikselin belirtilen doğru parçası üzerinde yer aldığı geri dönülüyor.
            return items.Count();
        }

        /// <summary>
        /// Belirtilen konumdan X ekseni üzerinde distance birim kadar uzakta olan Y Ekseninin yine (distance X 2 birim) uzunluğunda alanını tarayan method
        /// tarama işlemi için öncelikle hedef noktasından X eksenine bir dikme çekilecek şekilde hedef doğru parçasının tam orta noktasına sorgu yapılıyor
        /// sonrasında tesbit edilen orta noktaya +/- değerler verilerek orta noktanın bir alt ve bir üst noktaları dönülüyor. Bu işlem distance değeri kadar tekrarlanıyor
        /// </summary>
        /// <param name="x">belirlenen pikselin x ekseninde ki yerini tutan parametredir</param>
        /// <param name="y">belirlenen pikselin y ekseninde ki yerini tutan parametredir</param>
        /// <param name="distance">belirlenen pikselin merkez pikselden ne kadar uzakta olduğunu ve hedef doğru parçasının kaç birimden olusacağını belirten parametredir.</param>
        /// <returns></returns>
        private int CheckForTop(int x, int y, int distance)
        {
            // random yada manuel oluşturulan piksellerin tutulduğu liste içerisinde hedeflenen doğru parçası üzerinde yer alan piksel listesi çekiliyor
            var items = NoktaListesi.Where(item => item.XEkseni == x - distance && item.YEkseni > x - distance && item.YEkseni < x + distance);

            // listedeki piksel ve hedef pikseller arasındaki mesafeyi tutacak değişkene ilk değerinin setlenmesi
            double pointDistance = 0;

            // listeden gelen değer olup olmadığının sorgulanması. eğer değer varsa bu değerlerin tek tek hesaplanarak işleme alınması
            if (items.Count() > 0)
                foreach (var item in items)
                {
                    // pisagor teoremi ile eksenlerin uzunluklarına bağlı olarak hipotenüs değeri olan mesafenin hesaplanması yapılmaktadır.
                    pointDistance = Math.Sqrt(Math.Pow(item.XEkseni - HedefX, 2) + Math.Pow(item.YEkseni - HedefY, 2));

                    // yapılan hesaplamadan sonra ekranın sol tarafında yer alan listeye piksel bilgilerinin eklenmesi yapılıyor. 
                    lbEslesmeler.Items.Add($"checkForTop=> X:{item.XEkseni}, Y:{item.YEkseni}, Grup:{item.Grup}     Fark X:{item.XEkseni - HedefX}, Y{item.YEkseni - HedefY}, Uzaklık:{pointDistance}");

                    // eğer K değeri kadar noktaya henüz ulaşılmamışsa en yakın piksellerin tutulduğu listeye pikselin eklenme işlemi gerçekleştiriliyor
                    if (SecilenlerListesi.Count() <= KDegeri)
                    {
                        SecilenlerListesi.Add(new SelectedItem { Id = SecilenlerListesi.Count() + 1, Distance = pointDistance, Grup = item.Grup, MyListItemId = item.Id });

                        // Bu ekleme işleminden sonra en yakın olan noktaların diğerlerinden ayırt edilebilmesi için pikselin koordinat değerleri ve yeni değeri gönderilerek piksel kırmızı renge döndürülüyor
                        ChangeColor(item.XEkseni, item.YEkseni, Color.Red);
                    }
                }

            // işlem sonucunda ise kaç farklı pikselin belirtilen doğru parçası üzerinde yer aldığı geri dönülüyor.
            return items.Count();
        }

        /// <summary>
        /// berlirlenen distance değerlerine bakarak köşelerde yer alan piksellerin kontrolü sağlanmaktadır.
        /// </summary>
        /// <param name="x">belirlenen pikselin x ekseninde ki yerini tutan parametredir</param>
        /// <param name="y">belirlenen pikselin y ekseninde ki yerini tutan parametredir</param>
        /// <param name="distance">belirlenen pikselin merkez pikselden ne kadar uzakta olduğunu belirten parametredir.</param>
        /// <returns></returns>
        private int CheckForCorner(int x, int y, int distance)
        {
            // random yada manuel oluşturulan piksellerin tutulduğu liste içerisinde hedeflenen köşeler üzerinde yer alan piksel listesi çekiliyor
            var items = NoktaListesi.Where(item =>
                 (item.XEkseni == x - distance && item.YEkseni == y - distance) ||
                 (item.XEkseni == x + distance && item.YEkseni == y - distance) ||
                 (item.XEkseni == x - distance && item.YEkseni == y + distance) ||
                 (item.XEkseni == x + distance && item.YEkseni == y + distance)
             );

            // listedeki piksel ve hedef pikseller arasındaki mesafeyi tutacak değişkene ilk değerinin setlenmesi
            double pointDistance = 0;

            // listeden gelen değer olup olmadığının sorgulanması. eğer değer varsa bu değerlerin tek tek hesaplanarak işleme alınması
            if (items.Count() > 0)
                foreach (var item in items)
                {
                    // pisagor teoremi ile eksenlerin uzunluklarına bağlı olarak hipotenüs değeri olan mesafenin hesaplanması yapılmaktadır.
                    pointDistance = Math.Sqrt(Math.Pow(item.XEkseni - HedefX, 2) + Math.Pow(item.YEkseni - HedefY, 2));

                    // yapılan hesaplamadan sonra ekranın sol tarafında yer alan listeye piksel bilgilerinin eklenmesi yapılıyor. 
                    lbEslesmeler.Items.Add($"checkForCorner=> X:{item.XEkseni}, Y:{item.YEkseni}, Grup:{item.Grup}     Fark X:{item.XEkseni - HedefX}, Y{item.YEkseni - HedefY}, Uzaklık:{pointDistance}");

                    // eğer K değeri kadar noktaya henüz ulaşılmamışsa en yakın piksellerin tutulduğu listeye pikselin eklenme işlemi gerçekleştiriliyor
                    if (SecilenlerListesi.Count() <= KDegeri)
                    {
                        SecilenlerListesi.Add(new SelectedItem { Id = SecilenlerListesi.Count() + 1, Distance = pointDistance, Grup = item.Grup, MyListItemId = item.Id });

                        // Bu ekleme işleminden sonra en yakın olan noktaların diğerlerinden ayırt edilebilmesi için pikselin koordinat değerleri ve yeni değeri gönderilerek piksel kırmızı renge döndürülüyor
                        ChangeColor(item.XEkseni, item.YEkseni, Color.Red);
                    }
                }

            // işlem sonucunda ise kaç farklı pikselin belirtilen doğru parçası üzerinde yer aldığı geri dönülüyor.
            return items.Count();
        }

        /// <summary>
        /// pnlMatris üzerinde yer alan piksellerin temizlenmesi için kullanılan metoddur. Matris içerisinde control olduğu sürece ilk kontrol elemanını silecek sekilde döngüye alınmıştır.
        /// </summary>
        private void RemoveMatris()
        {
            // pnlMatris içerisine eklenilmiş hiç bir kontrol kalmayana kadar dönmeye devam edecek olan döngünün başlangıcı
            while (pnlMatris.Controls.Count > 0)
                // pnlMatris içerisinde yer alan 0 indisli kontrolün silinmesine yarayan metod
                pnlMatris.Controls.RemoveAt(0);
        }

        /// <summary>
        ///  Form ekranında default olarak eklenmiş olarak gelen pnlMatris panelimiz yer almakta. Matris ile ilgili işlemler bu panel içerisinde gerçekleşmekte
        ///  belirtilen matrisin uzunluğu gerçek piksel değerlerine bakılarak 600 piksel uzunluğundadır. Kullanıcıdan istemiş olduğumuz "matris size" değişkeni ile matris üzerinde bir satırda kaç piksel olacağı belirlenmektedir.
        ///  kullanıcıdan aldığımız piksel sayısına göre de dinamik olarak oluşturulacak olan piksellerin konumlandırılmaları ve boyutları setlenmektedir. 
        /// </summary>
        private void DrawMatris()
        {
            // matrisin bir pikselinin uzunluğuna setlenecek olan piksel miktarı width height değişkenine setlenmiştir.
            // örn dışarıdan girilen size değeri 20 olduğunu varsayarsak 600/20=30px olacak şekilde hesaplanır ve pnlMatrisimiz üzerinde yer alacak her bir pikselin uzunluğu 30px olarak belirlenmiş olur.
            int widthHeight = 600 / Uzaklik;

            // pnlMatrisimizin içerisinin belirtilen büyüklüklerdeki pikseller ile doldurulması için mxm adet piksel oluşturuluyor. ilk belirttiğimiz i sayacı burada kolon değerini temsil etmektedir.
            for (int i = 0; i < Uzaklik; i++)
            {
                // pnlMatrisimizin içerisine eklenen i kolonumuz içerisine yeni j satırlarının eklenmesi işlemi.
                for (int j = 0; j < Uzaklik; j++)
                {
                    // proje içerisinde bahsetmiş olduğumuz pikseller aslında birer c# windows form panellerinden oluşmakta. 
                    Panel pnl = new Panel();

                    // oluşturulan panellerin birbirinden ayrı olmasını sağlamak için görsel değerleri 2şer px küçültülerek ekrana yansıtılıyor.
                    pnl.Size = new Size(widthHeight - 2, widthHeight - 2);

                    // oluşturulan panellerin (0,0) dan başlayacak şekilde konumlandırılmalarının hesaplanım setlenmesi.
                    pnl.Location = new Point(i * widthHeight, j * widthHeight);

                    // belirtilen panellerin etrafının görülebilmesi için kontur değerinin setlenmesi
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    // herhangi bir renk ataması yapılmayan panellerin renk değerlerinin setlenmesi
                    pnl.BackColor = Color.White;

                    // oluşturulan panele daha sonra erişip değişiklik yapabilmek için name attributunde konum değerlerinin saklanması
                    pnl.Name = $"pnl{i}-{j}";

                    // eğer aktif bir sistem çağrısı varsa asenkron şekilde devam edebilmesi için pnlMatris içerisine yeni panelin eklenme işleminin delege edilmesi
                    if (pnlMatris.InvokeRequired)
                    {
                        pnlMatris.Invoke((MethodInvoker)delegate ()
                        {
                            // yeni oluşturulan panelin pnlMatris üzerine eklenmesi
                            pnlMatris.Controls.Add(pnl);
                        });
                    }
                    else
                    {
                        // çalışan bir sistem çağrısı bulunmadığı durumlarda pnlMatris üzerine yeni panelin eklenmesi
                        pnlMatris.Controls.Add(pnl);
                    }
                }
            }
        }

        /// <summary>
        /// Manuel olarak yeni bir piksel ekleme işlemi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEkle_Click(object sender, EventArgs e)
        {
            // form üzerinde yer alan x ekseni ve y ekseni değerleri girildikten sonra pnlMatris ve NoktaListesi içerisine yeni bir piksel değeri eklenmesi
            AddItem(Convert.ToInt32(txtXEkseni.Text), Convert.ToInt32(txtYEkseni.Text), cbGrup.Text);
        }

        /// <summary>
        /// yeni bir piksel eklenmek istendiğinde belirtilen koordinatlar üzerinde yeni bir piksel oluşturulmasını sağlayan metoddur
        /// </summary>
        /// <param name="x">integer olarak gönderilen bu değer pikselin x ekseni üzerindeki konumunu belirtmek üzere gönderilir</param>
        /// <param name="y">integer olarak gönderilen bu değer pikselin y ekseni üzerindeki konumunu belirtmek üzere gönderilir</param>
        /// <param name="grup">piksel için bir grup setlenmesi için gönderilen parametredir.</param>
        private void AddItem(int x, int y, string grup)
        {
            // uygulamanın başlangıcında boş olarak setlenmiş olan NoktaListesi içerisine yeni bir piksel eklemesinin yapılması
            NoktaListesi.Add(new MyListItem { Id = NoktaListesi.Count + 1, XEkseni = x, YEkseni = y, Grup = grup });

            // eklenen bu pikselin ekranın sağ tarafında bulunan listede yazdırılması
            lbItemList.Items.Add($"X:{x}, Y:{y}, Grup:{grup}");

            // eklenen pikselin renginin diğer piksellerden ayırt edilebilmesi için renginin yeşile dönmesinin sağlanması
            ChangeColor(x, y, Color.Green);
        }

        /// <summary>
        /// Bu method üzerinden kendisine gelen koordinatlardaki pikseli kendisine gelen renk ile yeniden renklendirme işlemi yapılıyor.
        /// </summary>
        /// <param name="x">x parametresi belirtilen pikselin hem x ekseni üzerindeki yerini hemde piksele Identity değer sağlamak amacı ile atamış olduğumuz name attribute değerini tesbit edebilmemiz için kullanılıyor.</param>
        /// <param name="y">y parametresi belirtilen pikselin hem y ekseni üzerindeki yerini hemde piksele Identity değer sağlamak amacı ile atamış olduğumuz name attribute değerini tesbit edebilmemiz için kullanılıyor.</param>
        /// <param name="color">piksel için yeni renk atanacak renk</param>
        /// <param name="force">proje yeniden başlatıldığında tüm renklerin sıfırlanıp piksellerin yeniden oluşturulmasını sağlamak için true gönderilmesi gereken değer.</param>
        private void ChangeColor(int x, int y, Color color, bool force = false)
        {
            // pnlMatrisimiz içerisinde yer alan controller birer birer inceleniyor.
            foreach (Control item in pnlMatris.Controls)
            {
                // Daha önce piksellerimizi oluştururken işaretlemiştik. Method parametrelerinden aldığımız değerler ile işaretlediğimiz pikseli ulaşıp ulaşmadığımızı kontrol ediyoruz.
                if (item.Name == $"pnl{x}-{y}")
                {
                    // buradaki sorgu hedef üzerinde eğer birebir işaretlenmiş değer olup olmadığını kontrol ediyor. 
                    // örneğin konumu(5,5) olan bir piksel rastgele atamalar sonucunda işaretlenmiş olsun. O anki aktif rengi yeşil olacaktır. Daha sonrasında atayacağımız hedef konumu(5,5) seçersek
                    // burasının rengi yeşilden maviye dönecektir ve bu nokta aslında hedefimize en yakın nokta olacaktır.
                    if (item.BackColor == Color.Blue && !force)
                    {
                        // eğer hedef noktamız ve aynı kordinatlar üzerinde yer alan daha önce işaretlenmiş noktamız üst üste gelirse belirtilen pikselin rengi maviden mavi ve kırmızının ortak noktası olan mora dönmektedir.
                        item.BackColor = Color.Purple;
                    }
                    else
                    {
                        // aksi durumlarda ise parametre olarak gönderilen color değeri pikselimizin yeni değeri olarak atanmaktadır.
                        item.BackColor = color;
                        item.BringToFront();
                    }
                }
            }
        }

        /// <summary>
        /// Belirtilen hedef noktaya en yakın k adet pikselin saptanmasını sağlayan butonundur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {

            // Daha önceden yapılan işlem için eskiye ait verileri listBox üzerinden silme işlemi yapılıyor.
            lbEslesmeler.Items.Clear();

            // Daha önceden yapılan işlem için eskiye ait verilerin belleğe alınmasını sağladığımız SecilenlerListesi yeniden oluşturuluyor.
            SecilenlerListesi = new List<SelectedItem>();

            // Form üzerinde yer alan KDeğeri inputu içerisine bir değer girilip girilmediği kontrol ediliyor.
            if (!string.IsNullOrEmpty(txtKDegeri.Text))
                // Eğer bir değer girildiyse girilen değer int değer olarak parse edilip saklanıyor eğer bir değer girilmediyse varsayılan değer olarak 1  (yani kendisine en yakın noktayı bulmak üzere) yapılandırması tamamlanıyor.
                KDegeri = !string.IsNullOrEmpty(txtKDegeri.Text) ? Convert.ToInt32(txtKDegeri.Text) : 1;

            // Form üzerinde yer alan X ekseni inputu içerisine bir değer girilip girilmediği kontrol ediliyor.
            if (!string.IsNullOrEmpty(txtHedefX.Text))
                // Eğer bir değer girildiyse girilen değer int değer olarak parse edilip saklanıyor eğer bir değer girilmediyse varsayılan değer olarak toplam x ekseni uzunluğunun yarısı alınarak yapılandırması tamamlanıyor.
                HedefX = !string.IsNullOrEmpty(txtHedefX.Text) ? Convert.ToInt32(txtHedefX.Text) : Uzaklik / 2;

            // Form üzerinde yer alan Y ekseni inputu içerisine bir değer girilip girilmediği kontrol ediliyor.
            if (!string.IsNullOrEmpty(txtHedefY.Text))
                // Eğer bir değer girildiyse girilen değer int değer olarak parse edilip saklanıyor eğer bir değer girilmediyse varsayılan değer olarak toplam  y ekseni uzunluğunun yarısı alınarak yapılandırması tamamlanıyor.
                HedefY = Convert.ToInt32(txtHedefY.Text);

            // Yukarıda yer alan değerleri özetlemek gerekirse bütün alanlar boş geçildiğinde hedef noktamız matris dizimizin ortasında yer alan piksel ve K değerimiz ise yanına en yakın noktayı bulmak üzere yapılandırılmış oluyor.

            // Hedef noktasının diğer noktalardan ayrılmasını sağlamak amacıyla hedef noktamız maviye boyanıyor.
            ChangeColor(HedefX, HedefY, Color.Blue);

            // 0 dan başlayıp toplam uzaklık değeri kadar sorgulama yapacak olan döngü
            // eğer değer 0 değil de birden başlatılırsa hedef noktası olarak konulan değer daha önceden işaretlenmiş bir piksele denk geliyorsa bunu sorgulamadan işlemlere devam edecektir.
            for (int i = 0; i < Uzaklik; i++)
            {
                // hedef noktadan dışarıya doğru yayılan dalgada hedef noktanın sağ tarafında yer alan i birim uzaklıktaki 2i birim uzunluğunda ve hedef noktasına dik olan doğru parçasının inceleme işleminin başlatılması
                CheckForRight(HedefX, HedefY, i);

                // hedef noktadan dışarıya doğru yayılan dalgada hedef noktanın alt tarafında yer alan i birim uzaklıktaki 2i birim uzunluğunda ve hedef noktasına dik olan doğru parçasının inceleme işleminin başlatılması
                CheckForBottom(HedefX, HedefY, i);

                // hedef noktadan dışarıya doğru yayılan dalgada hedef noktanın sol tarafında yer alan i birim uzaklıktaki 2i birim uzunluğunda ve hedef noktasına dik olan doğru parçasının inceleme işleminin başlatılması
                CheckForLeft(HedefX, HedefY, i);

                // hedef noktadan dışarıya doğru yayılan dalgada hedef noktanın üst tarafında yer alan i birim uzaklıktaki 2i birim uzunluğunda ve hedef noktasına dik olan doğru parçasının inceleme işleminin başlatılması
                CheckForTop(HedefX, HedefY, i);

                // hedef noktadan dışarıya doğru yayılan dalgada hedef noktanın köşelerinde yer alan piksellerin inceleme işleminin başlatılması
                CheckForCorner(HedefX, HedefY, i);

                // incelenen piksellerin tekrar incelenmemesini sağlamak amacıyla yukarıda belirtilen doğru parçalarının birleşim noktalarında yer alan pikseller kenarlar ile birlikte incelenmemiştir.
                // orta noktaya en uzak değer olan bu pikseller kenarların sırayla taraması bittikten sonra tek tek incelenmiştir.


                // gerekli sayıda ki seçilmiş noktaya ulaşıldığında döngünün sonlandırılması ve sonlandırma esnasında yapılacak işlemlerin tamamlanması için her turda seçilen nokta sayısının k değerine ulaşıp ulaşmadığı kontrol ediliyor.
                if (SecilenlerListesi.Count() >= KDegeri)
                {
                    // eğer istenilen sayıda seçilmiş nokta toplandıysa döngüyü sonlandırmak için i sayacının değeri max integer değer olarak yeniden setleniyor.
                    i = Uzaklik;

                    // işlem tamamlandıktan sonra verilecek mesajın oluşturulması için message değişkeni boş değer olarak setleniyor.
                    string message = string.Empty;

                    // seçilenler listesinde yer alan (belirtilen hedef noktaya en yakın K adet pikselin saklandığı) liste içerisinden bu piksellerin hangi gruplara ait olduğu ve bu pixsellerin hedef noktasına olan uzaklıklarını işlem sonunda ekrana basmak için bir değişkene atıyoruz.
                    SecilenlerListesi.ForEach(x => message += "\n" + x.Grup + " : " + x.Distance);

                    // hazırlanan mesajın mesaj box içerisinde gösterilmesi
                    MessageBox.Show(message);
                }
            }
        }

        /// <summary>
        /// Random olarak istenilen sayıda piksel oluşturup sisteme kaydetmek üzere hazırlanmış bir kısayol tuşudur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRnd_Click(object sender, EventArgs e)
        {
            // dışarıdan bir değer girilmemesi durumunda üretilecek olan piksel sayısının setlenmesi.
            int adet = 20;

            // random oluşturma esnasında piksel matrisinin yeniden boyutlandırılması için bir değer girilip girilmediğinin kontrol edilmesi
            if (!string.IsNullOrEmpty(txtSize.Text))
            {
                // eğer bir değer girildiyse girilen bu değerin hem matris uzunluğunu hemde sorgu derinliğini belirleyen Uzaklik değişkenine setlenmesi.
                Uzaklik = Convert.ToInt32(txtSize.Text);

                // yeniden oluşturulacak olan matris serisinden önce eski matrisin temizlenmesi
                RemoveMatris();

                // yeni matris sisteminin oluşturulması
                DrawMatris();
            }

            // dışarıdan beklenen piksel değeri alanının boş olup olmadığını kontrol eden sorgu
            if (!string.IsNullOrEmpty(txtRnd.Text))
                // eğer bu değer boş gelmezse yeni değer eski değerin üzerine setleniyor.
                adet = Convert.ToInt32(txtRnd.Text);

            // random seçim yapmamızı sağlayan yapıdan yeni bir nesne örneğinin alınması.
            Random rnd = new Random();

            // daha önceden belirlenen adet kadar iterasyon yapacak olan for döngüsünün oluşturulması.
            for (int i = 0; i < adet; i++)
            {
                // random sınıfının bir metodu olan next metodunun kullanılarak yeni bir x koordinatının belirlenmesi ve setlenmesi
                // next metodunun kullanımı ise birinci ve ikinci belirtilen değerler arasında yeni bir sayı seçecek şekilde yapılandırılıp dönecek olan integer değerin karşılanması şeklindedir.
                int x = rnd.Next(0, Uzaklik);

                // random sınıfının bir metodu olan next metodunun kullanılarak yeni bir y koordinatının belirlenmesi ve setlenmesi
                int y = rnd.Next(0, Uzaklik);

                // belirtilen koordinatlar üzerinde daha önceden oluşturulmuz olan bir piksel değeri var ise yeni oluşturulan piksel değeri sisteme dahil edilmemektedir.
                if (NoktaListesi.Where(item => item.XEkseni == x && item.YEkseni == y).Count() > 0 || NoktaListesi.Where(item => item.XEkseni == y && item.YEkseni == x).Count() > 0)
                {
                    // bu işlem ise for döngüsünü fazladan bir tur daha döndürerek yapılmaktadır.
                    i--;
                }
                else
                {
                    // random nesnesi üzerinden 0,1 değerlerinden birisinin rastgele seçilip pikselin hangi gruba ait olduğu belirlenmiştir.
                    int grupId = rnd.Next(0, 2);

                    // bütün propertileri hazırlanmış olan bu piksel AddItem metoduna gönderilerek yeni bir piksel oluşturması yapılmaktadır.
                    AddItem(x, y, cbGrup.Items[grupId].ToString());
                }
            }
        }

    }

    // bir piksele karşılık gelecek class yapısı
    public class MyListItem
    {
        // her bir pikselin benzersiz anahtar propertysi
        public int Id { get; set; }

        // pikselin x ekseni üzerindeki koordinatını belirleyen property
        public int XEkseni { get; set; }

        // pikselin y ekseni üzerindeki koordinatını belirleyen property
        public int YEkseni { get; set; }

        // piksel eğer bir gruba dahil ise bu grubun değerini saklayacak olan property
        public string Grup { get; set; }
    }

    // pikseller içerisinden hedef noktaya en yakın piksel değerinin saklanacağı class yapısı
    public class SelectedItem
    {
        // her bir değerin benzersiz anahtar propertysi
        public int Id { get; set; }

        // matris üerindeki pikselin seçili piksel ile ilişkilendirilmesini sağlayacak olan property
        public int MyListItemId { get; set; }

        // seçili pikselin hedef piksel ile olan uzaklığını saklayan property
        public double Distance { get; set; }

        // seçili pikselin grup bilgisine sorgu yapılmadan erişebilmek için kısayol olarak kullanılan grup propertysi
        public string Grup { get; set; }
    }
}
