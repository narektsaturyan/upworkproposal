
// AJAX search call
$("#sinput").keyup(function () {
    var searchString = $("#sinput").val();
    console.log(searchString);
    if (searchString === "") {
        $.ajax({
            url: "/Ajax/Search/?keyone=%00",
            success: function (data) {
                $("#ContactListContainerAtAddContact").empty();
                $("#ContactListContainerAtAddContact").html(data);
            }
        })
    }
    else if (searchString.includes(" ")) {
        var searchArray = searchString.split(" ");

        $.ajax({
            url: "/Ajax/Search/?keyone=" + searchArray[0] + "&keytwo=" + searchArray[1],
            success: function (data) {
                $("#ContactListContainerAtAddContact").empty();
                $("#ContactListContainerAtAddContact").html(data);
            }
        })
    }
    else {

        $.ajax({
            url: "/Ajax/Search/?keyone=" + searchString,
            success: function (data) {
                $("#ContactListContainerAtAddContact").empty();
                $("#ContactListContainerAtAddContact").html(data);
            }
        })
    }
})

