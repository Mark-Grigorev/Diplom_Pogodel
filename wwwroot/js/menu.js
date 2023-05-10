 $(document).ready(function () {
            $('#GetIndexParticle').click(function (e) {
                $('#MainPartialViews').load('@Url.Action("Index")')
            });

            $('#GetAboutUsParticle').click(function (e) {
                $('#MainPartialViews').load('@Url.Action("AboutUsParticle")')
            });
            
            $('#GetAuthorizationPartiacle').click(function (e) {
                $('#MainPartialViews').load('@Url.Action("AuthorizationParticle")')
            });

        });