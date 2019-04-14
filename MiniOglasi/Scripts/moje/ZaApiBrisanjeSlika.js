$(document).ready(function () {
    var vecKliknutaSlika = false;

    $("#slikeZaBrisanjeDiv").on("click", function (e) {
        if (vecKliknutaSlika == false) {
            vecKliknutaSlika = true

            var slikaZaBrisanje = e.target;
            var slikaId;

            if (slikaZaBrisanje.classList.contains("js-delete-img")) {
                slikaId = slikaZaBrisanje.dataset["slikaId"];

                $.ajax({
                    url: `/api/delete-sliku/${slikaId}`,
                    method: "DELETE",
                    success: function () {
                        slikaZaBrisanje.remove();
                    },
                    error: function (err) {
                        //console.log(err)
                    },
                    complete: function () {
                        vecKliknutaSlika = false;
                    }
                });
            }
        }
    })
})