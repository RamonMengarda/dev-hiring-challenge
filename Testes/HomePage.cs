using Atata;

namespace Testes
{
    using _ = HomePage;

    [Url("signin")]
    [VerifyTitle]
    [VerifyH1]
   public class HomePage : Page<_>
    {
        public CheckBox<_> CheckBox0 { get; private set; }
        public CheckBox<_> CheckBox1 { get; private set; }
        public CheckBox<_> CheckBox2 { get; private set; }
        public CheckBox<_> CheckBox3 { get; private set; }
        public CheckBox<_> CheckBox4 { get; private set; }
        public CheckBox<_> CheckBox5 { get; private set; }
        public CheckBox<_> CheckBox6 { get; private set; }
        public CheckBox<_> CheckBox7 { get; private set; }
        public CheckBox<_> CheckBox8 { get; private set; }
        public CheckBox<_> CheckBox9 { get; private set; }
        public CheckBox<_> CheckBox10 { get; private set; }
        public CheckBox<_> CheckBox11 { get; private set; }
        public CheckBox<_> CheckBox12 { get; private set; }
        public CheckBox<_> CheckBox13 { get; private set; }
        public CheckBox<_> CheckBox14 { get; private set; }     
        public CheckBox<_> CheckBox15 { get; private set; }
        public CheckBox<_> CheckBox16 { get; private set; }
        public CheckBox<_> CheckBox17 { get; private set; }
        public Button<_> BtnSearch { get; private set; }
        public Button<_> BtnFavorite0 { get; private set; }
        public Link<_> FavoriteLink { get; private set; }
    }
}
