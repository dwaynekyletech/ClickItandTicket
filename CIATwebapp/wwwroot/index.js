function CIATwebapp() {

    //Get elements

    var textSearch = document.getElementById("floatingInput");
    var textSearch2 = document.getElementById("floatingPassword");
    var buttonSearch = document.getElementById("button-search");

    buttonSearch.addEventListener("click", searchUsers);

    function searchUsers() {

        var url = 'http://localhost:5079/SearchUsers?search=' + textSearch.value + '&search2=' + textSearch2.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchUsers;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterSearchUsers() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        alert("Hello World");
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };

}
CIATwebapp();