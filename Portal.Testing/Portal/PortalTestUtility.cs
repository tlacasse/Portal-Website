using Portal.Models.Portal;

namespace Portal.Testing.Portal {

    public static class PortalTestUtility {

        public static Icon GetIcon() {
            return new Icon() {
                Id = 100,
                Name = "Test Icon",
                Image = "png",
                Link = "hello.com"
            };
        }

    }

}
