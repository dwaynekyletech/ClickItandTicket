function CIATwebapp() {

    //Get elements

    var textSearch = document.getElementById("floatingInput");
    var textSearch2 = document.getElementById("floatingPassword");
    var buttonSearch = document.getElementById("button-search");
    var buttonCreateATicket = document.getElementById("create")
    var buttonHome = document.getElementById("go-home")
    var buttonSignUp = document.getElementById("signup")

    buttonSearch.addEventListener("click", searchUsers)
    buttonCreateATicket.addEventListener("click", clickCreateATicket)
    buttonHome.addEventListener("click", clickHome)
    buttonSignUp.addEventListener("click", clickSignUp)

    function clickHome() {
        var pageSignin = document.getElementById("signin")
        pageSignin.classList.remove("visually-hidden")
        var pageCreateATicket = document.getElementById("create-ticket")
        pageCreateATicket.classList.add("visually-hidden")
        var pageSignUp = document.getElementById("signuplink")
        pageSignUp.classList.add("visually-hidden")
    }

    function clickSignUp() {
        var pageSignUp = document.getElementById("signuplink")
        pageSignUp.classList.remove("visually-hidden")
        var pageSignin = document.getElementById("signin")
        pageSignin.classList.add("visually-hidden")
        var pageCreateATicket = document.getElementById("create-ticket")
        pageCreateATicket.classList.add("visually-hidden")
    }

    function clickCreateATicket() {
        var pageSignin = document.getElementById("signin")
        pageSignin.classList.add("visually-hidden")
        var pageSignUp = document.getElementById("signuplink")
        pageSignUp.classList.add("visually-hidden")
        var pageCreateATicket = document.getElementById("create-ticket")
        pageCreateATicket.classList.remove("visually-hidden")

    }

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
                        // alert("API Error: " + response.message);
                        buttonSearch.addEventListener("click", searchCustomers)
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };

    function searchCustomers() {

        var url = 'http://localhost:5079/SearchCustomers?search=' + textSearch.value + '&search2=' + textSearch2.value;

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
                        alert("Goodbye World");
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