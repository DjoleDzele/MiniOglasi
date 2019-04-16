$(document).ready(function () {
    var podaci = $("#script-popunjavanje-automodela").data();
    var izborAutoMarkeDropdown = document.querySelector("#AutoOglas_MarkaAutaId");  // $("#marke-auta-dropdownlist");
    var izborAutoModelaDropdown = document.querySelector("#AutoOglas_ModelAutaId"); // $("#modeli-auta-dropdownlist");

    popuniTabeluAutoModela(izborAutoMarkeDropdown.value);

    function popuniTabeluAutoModela(autoMarkaId) {
        $.ajax({
            url: `/api/auto-modeli/${autoMarkaId}`,
            method: "GET",
            success: function (data) {
                izborAutoModelaDropdown.innerHTML = "";

                data.forEach(function (x) {
                    var opcijaZaModel =
                        `<option ${x.Id == podaci.scriptPopunjavanjeAutomodela ? selected = "selected" : ""} value="${x.Id}">${x.AutoModel}</option>`
                    izborAutoModelaDropdown.innerHTML += opcijaZaModel;
                })
            },
            error: function (err) {
                console.log(err)
            }
        })
    }

    izborAutoMarkeDropdown.addEventListener("change", function (e) {
        var izabranaMarkaAuta = e.target.value;

        popuniTabeluAutoModela(izabranaMarkaAuta);
    });
})