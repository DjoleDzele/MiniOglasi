$(document).ready(function () {
    var podaci = $("#script-popunjavanje-automodela").data();
    var izborAutoMarkeDropdown = document.querySelector("#AutoOglas_MarkaAutaId");  // $("#marke-auta-dropdownlist");
    var izborAutoModelaDropdown = document.querySelector("#AutoOglas_ModelAutaId"); // $("#modeli-auta-dropdownlist");

    popuniTabeluAutoModela(izborAutoMarkeDropdown.value);

    izborAutoMarkeDropdown.addEventListener("change", function (e) {
        var izabranaMarkaAuta = e.target.value;

        popuniTabeluAutoModela(izabranaMarkaAuta);
    });

    function popuniTabeluAutoModela(autoMarkaId) {
        $.ajax({
            url: `/api/auto-modeli/${autoMarkaId}`,
            method: "GET",
            success: function (data) {
                izborAutoModelaDropdown.innerHTML = "";

                data.forEach(function (x) {
                    var selected = x.Id == podaci.modelAutaId ? selected = "selected" : "";
                    var opcijaZaModel = `<option ${selected} value="${x.Id}">${x.AutoModel}</option>`
                    izborAutoModelaDropdown.innerHTML += opcijaZaModel;
                })
            },
            error: function (err) {
                console.log(err.responseText)
            }
        })
    }
})