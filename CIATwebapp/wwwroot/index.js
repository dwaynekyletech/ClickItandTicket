function CIATwebapp() {

    //Get elements

    var textSearch = document.getElementById("floatingInput");
    var textSearch2 = document.getElementById("floatingPassword");
    var buttonSearch = document.getElementById("button-search2");
    var buttonCreateATicket = document.getElementById("create")
    var buttonHome = document.getElementById("go-home")
    var buttonSignUp = document.getElementById("signup")
    var buttonInsert = document.getElementById("button-insert");
    var buttonDelete = document.getElementById("button-delete");

    buttonSearch.addEventListener("click", searchUsers)
    buttonCreateATicket.addEventListener("click", clickCreateATicket)
    buttonHome.addEventListener("click", clickHome)
    buttonSignUp.addEventListener("click", clickSignUp)
    buttonInsert.addEventListener("click", insertTicket);
    buttonDelete.addEventListener("click", handleButtonDeleteClick);



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


    function searchUsers(userId) {


        var url = 'http://localhost:5079/SearchUsers';

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchUsers;
        xhr.open('POST', url);
        xhr.setRequestHeader('Content-Type', 'application/json');
        var body = {
            "userName": textSearch.value,
            "password": textSearch2.value,
            "userid": userId
        };
        xhr.send(JSON.stringify(body));
        // xhr.send(null);

        function doAfterSearchUsers() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {
                    var response = JSON.parse(xhr.responseText);

                    if (response.user_id > 0) {

                        alert(response.user_id);


                    } else {
                        // alert("API Error: " + response.message);
                        buttonSearch.addEventListener("click", searchCustomers());
                    }
                    var response = JSON.parse(xhr.responseText);
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };

    function searchCustomers(customerid) {

        var url = 'http://localhost:5079/SearchCustomers';

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchCustomers;
        xhr.open('POST', url);
        xhr.setRequestHeader('Content-Type', 'application/json');
        var body = {
            "userName": textSearch.value,
            "password": textSearch2.value,
            "customerid": customerid
        };
        xhr.send(JSON.stringify(body));
        // xhr.send(null);

        function doAfterSearchCustomers() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.customer_id > 0) {

                        alert(response.customer_id);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };

    function getTickets() {

        var url = "http://localhost:5079/GetTickets?customer_id=1";

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterGetTickets;
        xhr.open("GET", url);

        xhr.send(null);

        function doAfterGetTickets() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showTickets(response.tickets);
                        // alert("Hello World")
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };
    function insertTicket() {

        var textSubject = document.getElementById("text-insert-Subject");
        var textDescription = document.getElementById("text-insert-Description");
        var ePriority = document.getElementById("priority-dropdown");
        // var value = ePriority.value;
        var textPriority = ePriority.options[ePriority.selectedIndex].text;

        var url = "http://localhost:5079/InsertTicket?TicketSubject=" + textSubject.value + "&ticketdescription=" + textDescription.value + "&ticketstatus=open&ticketpriority=" + ePriority.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterInsertEmployee;
        xhr.open("GET", url);
        xhr.send(null);

        function doAfterInsertEmployee() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        getTickets(response.tickets);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }

        var formInsert = document.getElementById("form-insert");
        // formInsert.classList.add("visually-hidden");
        // buttonShowInsertForm.classList.remove("visually-hidden");

        textSubject.value = "";
        textDescription.value = "";
    }
    function handleTicketTableDeleteClick(e) {
        var ticket_id = e.target.getAttribute("data-ticket-id");
        //alert("you want to delete employee " + employeeId)
        deleteTicket(ticket_id);
    }

    function handleButtonDeleteClick() {
        var textTicketId = document.getElementById("text-delete-ticket-id");
        deleteTicket(textTicketId.value);
    }

    function deleteTicket(ticket_id) {

        var url = "http://localhost:5079/DeleteTicket?ticket_id=" + ticket_id;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterDeleteEmployee;
        xhr.open("GET", url);
        xhr.send(null);

        function doAfterDeleteEmployee() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        getTickets(response.tickets);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }

    }

    function showTickets(tickets) {
        var ticketTableText = "<table class='table table-striped table-sm'><thead><tr><th scope='col'>Ticket ID</th><th scope='col'>Subject</th><th scope='col'>Description</th><th scope='col'>Status</th><th scope='col'>Priority</th><th scope='col'></th><th class='button-column'></th></tr></thead><tbody>";

        for (var i = 0; i < tickets.length; i++) {
            var ticket = tickets[i];

            // var employeeSalary = (employee.salary === null) ? "" : employee.salary;

            ticketTableText = ticketTableText + "<tr><th scope='row'>" + ticket.ticket_id + "</th><td id='emp-" + ticket.ticket_id + "-subject'>" + ticket.ticketSubject + "</td><td id='emp-" + ticket.ticketId + "-description'>" + ticket.ticketDescription + "</td><td id='emp-" + ticket.ticket_id + "-status'>" + ticket.ticketStatus + "</td><td id='emp-" + ticket.ticket_id + "-subject'>" + ticket.ticketPriority + "</td><td><div class='row g-2'><div class='col-auto'><button type='button' data-ticket-id='" + ticket.ticket_id + "' class='btn btn-outline-primary btn-sm btn-ticket-table-update'>Update</button></div><div class='col-auto'><button id='' type='button' data-ticket-id='" + ticket.ticket_id + "' class='btn btn-outline-primary btn-sm btn-ticket-table-delete'>Delete</button></div></div></td></tr>";
        }

        ticketTableText = ticketTableText + "</tbody></table>";

        var ticketTable = document.getElementById("ticket-table");
        ticketTable.innerHTML = ticketTableText;

        var updateButtons = document.getElementsByClassName("btn-ticket-table-update");

        // for (var i = 0; i < updateButtons.length; i++) {
        //     var updateButton = updateButtons[i];
        //     updateButton.addEventListener("click", handleTicketTableUpdateClick);
        // }

        var deleteButtons = document.getElementsByClassName("btn-ticket-table-delete");

        for (var i = 0; i < deleteButtons.length; i++) {
            var deleteButton = deleteButtons[i];
            deleteButton.addEventListener("click", handleTicketTableDeleteClick);
        }
    }

    getTickets();
}
CIATwebapp();