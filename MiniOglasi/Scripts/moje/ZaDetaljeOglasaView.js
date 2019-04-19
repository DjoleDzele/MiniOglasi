$(document).ready(function () {
    var podaci = $("#detaljiSkripta").data();

    if (podaci.requestAuthenticated) {
        var srceDugme = $("#srce-dugme")

        var mozeDaKlikneZvezdu = true
        if (mozeDaKlikneZvezdu) {
            mozeDaKlikneZvezdu = false

            srceDugme.on("click", function (e) {
                var srce = e.target
                if (srce.classList == "glyphicon glyphicon-heart") {
                    $.ajax({
                        url: `/api/omiljeni-oglas/${podaci.oglasId}`,
                        method: "DELETE",
                        success: function () {
                            srce.classList = "glyphicon glyphicon-heart-empty"
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
                            srce.classList = "glyphicon glyphicon-heart"
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