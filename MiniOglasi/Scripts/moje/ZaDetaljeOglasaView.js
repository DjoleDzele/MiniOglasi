$(document).ready(function () {
    var podaci = $("#detaljiSkripta").data();

    if (podaci.requestAuthenticated) {
        var zvezdaDugme = $("#zvezda-dugme")

        var mozeDaKlikneZvezdu = true
        if (mozeDaKlikneZvezdu) {
            mozeDaKlikneZvezdu = false

            zvezdaDugme.on("click", function (e) {
                var zvezda = e.target
                if (zvezda.classList == "glyphicon glyphicon-star") {
                    $.ajax({
                        url: `/api/omiljeni-oglas/${podaci.oglasId}`,
                        method: "DELETE",
                        success: function () {
                            zvezda.classList = "glyphicon glyphicon-star-empty"
                        },
                        error: function (err) {
                            //console.log(err)
                        },
                        complete: function () {
                            mozeDaKlikneZvezdu = true
                        }
                    });
                }
                else {
                    $.ajax({
                        url: `/api/omiljeni-oglas/${podaci.oglasId}`,
                        method: "POST",
                        success: function () {
                            zvezda.classList = "glyphicon glyphicon-star"
                        },
                        error: function (err) {
                            //console.log(err)
                        },
                        complete: function () {
                            mozeDaKlikneZvezdu = true
                        }
                    });
                }
            })
        }

        if (podaci.userId == podaci.autorOglasaId) {
            var mozeDaKlikneDugme = true;
            if (mozeDaKlikneDugme) {
                mozeDaKlikneDugme = false;

                $(".dugme-delete").on("click", function () {
                    bootbox.confirm({
                        message: "Izbrisi oglas?",
                        buttons: {
                            cancel: {
                                label: 'Ne',
                            },
                            confirm: {
                                label: 'Da',
                                className: 'btn-danger'
                            }
                        },
                        className: "heartBeat animated",
                        size: "small",
                        callback: function (result) {
                            mozeDaKlikneDugme = true;
                            if (result) {
                                $.ajax({
                                    url: `/api/delete-oglas/${podaci.oglasId}`,
                                    method: "DELETE",
                                    success: function () {
                                        window.location.href = `/${podaci.kategorijaOglasa}/`
                                    },
                                    error: function (err) {
                                        //console.log(err);
                                    }
                                });
                            }
                        }
                    });
                })

                $(".dugme-edit").on("click", function () {
                    bootbox.confirm({
                        message: "Izmeni oglas?",
                        buttons: {
                            cancel: {
                                label: 'Ne',
                            },
                            confirm: {
                                label: 'Da',
                                className: 'btn-danger'
                            }
                        },
                        className: "heartBeat animated",
                        size: "small",
                        callback: function (result) {
                            mozeDaKlikneDugme = true;
                            if (result) {
                                window.location.href = `${podaci.urlEdit}`
                            }
                        }
                    });
                })
            }
        }
    }
})