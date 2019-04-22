$(document).ready(function() {

    Pagination();
    ConfirmDelect();
    
    $(".search").keyup(function() {
        var searchString = $(this).val();

        $.ajax({
            type: "Get",
            url: "/Candidate/Search",
            data: { searchString: searchString },
            success: function(result) {
                $(".content").remove();
                if (result.length > 0) {
                    result.forEach(function(row) {
                        $("tbody").append("<tr class='content" + " " + row.id + "'" + ">" + "</tr>");
                        $("." + row.id).append("<td>" + row.name + "</td>");
                        $("." + row.id).append("<td>" + row.vacancyName + "</td>");

                        if (row.phone != null) {
                            $("." + row.id).append("<td>" + row.phone + "</td>");
                        } else {
                            $("." + row.id).append("<td></td>");
                        }

                        if (row.avatar != null) {
                            $("." + row.id).append("<td>" + "<img src='" + row.avatar + "'" + "/>" + "</td>");
                        } else {
                            $("." + row.id).append("<td></td>");
                        }

                        $("." + row.id).append("<td><a class='text-primary' href='/Candidate/Edit/" +
                            row.id +
                            "'" +
                            ">" +
                            "<i class='fas fa-user-edit'></i></a></td>");
                        $("." + row.id).append("<td><a class='text-danger bt-remove' href='/Candidate/Delect/" +
                            row.id +
                            "'" +
                            ">" +
                            "<i class='fas fa-user-times'></i></a></td>");
                    });
                    
                    Pagination();
                } else {
                    $(".pagination-item").remove();
                    $("tbody").append("<tr class='content'></tr>");
                    $(".content").append("<td></td>" + "<td colspan='2'>No result to found</td>" + "<td></td>");
                }
                
            }
        });
    });

    $(".filter").click(function() {
        var vacancyId = $(this).val();

        $.ajax({
            type: "Get",
            url: "/Candidate/Filter",
            data: { vacancyId: vacancyId },
            success: function(result) {
                $(".content").remove();
                if (result.length > 0) {
                    result.forEach(function(row) {
                        $("tbody").append("<tr class='content" + " " + row.id + "'" + ">" + "</tr>");
                        $("." + row.id).append("<td>" + row.name + "</td>");
                        $("." + row.id).append("<td>" + row.vacancyName + "</td>");

                        if (row.phone != null) {
                            $("." + row.id).append("<td>" + row.phone + "</td>");
                        } else {
                            $("." + row.id).append("<td></td>");
                        }

                        if (row.avatar != null) {
                            $("." + row.id).append("<td>" + "<img src='" + row.avatar + "'" + "/>" + "</td>");
                        } else {
                            $("." + row.id).append("<td></td>");
                        }

                        $("." + row.id).append("<td><a class='text-primary' href='/Candidate/Edit/" +
                            row.id +
                            "'" +
                            ">" +
                            "<i class='fas fa-user-edit'></i></a></td>");
                        $("." + row.id).append("<td><a class='text-danger bt-remove' href='/Candidate/Delect/" +
                            row.id +
                            "'" +
                            ">" +
                            "<i class='fas fa-user-times'></i></a></td>");
                    });

                    Pagination();
                } else {
                    $(".pagination-item").remove();
                    $("tbody").append("<tr class='content'></tr>");
                    $(".content").append("<td></td>" + "<td colspan='2'>No result to found</td>" + "<td></td>");
                }
            }
        });
    });

    $(".sort").click(function() {
        var sortType = $(this).val();

        $.ajax({
            type: "Get",
            url: "/Candidate/Sort",
            data: { sortType: sortType },
            success: function(result) {
                $(".content").remove();
                if (result.length > 0) {
                    result.forEach(function(row) {
                        $("tbody").append("<tr class='content" + " " + row.id + "'" + ">" + "</tr>");
                        $("." + row.id).append("<td>" + row.name + "</td>");
                        $("." + row.id).append("<td>" + row.vacancyName + "</td>");

                        if (row.phone != null) {
                            $("." + row.id).append("<td>" + row.phone + "</td>");
                        } else {
                            $("." + row.id).append("<td></td>");
                        }

                        if (row.avatar != null) {
                            $("." + row.id).append("<td>" + "<img src='" + row.avatar + "'" + "/>" + "</td>");
                        } else {
                            $("." + row.id).append("<td></td>");
                        }

                        $("." + row.id).append("<td><a class='text-primary' href='/Candidate/Edit/" +
                            row.id +
                            "'" +
                            ">" +
                            "<i class='fas fa-user-edit'></i></a></td>");
                        $("." + row.id).append("<td><a class='text-danger bt-remove' href='/Candidate/Delect/" +
                            row.id +
                            "'" +
                            ">" +
                            "<i class='fas fa-user-times'></i></a></td>");
                    });
                    
                    Pagination();
                }
            }
        });
    });
    
});

function Pagination() {
    var items = $("table .content");
    var numItems = items.length;
    var perPage = 3

    items.slice(perPage).hide();

    $('.pagination-item').pagination({
        items: numItems,
        itemsOnPage: perPage,
        prevText: "&laquo;",
        nextText: "&raquo;",
        onPageClick: function (pageNumber) {
            var showFrom = perPage * (pageNumber - 1);
            var showTo = showFrom + perPage;
            items.hide().slice(showFrom, showTo).show();
        }
    });
}

function ConfirmDelect() {
    $(".bt-remove").click(function() {
        if (confirm("Are you sure?") == false) {
            return false;
        }
    });
}