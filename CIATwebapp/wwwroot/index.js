function CIATwebapp() {

    //Get elements

    var textSearch = document.getElementById("floatingInput");
    var textSearch2 = document.getElementById("floatingPassword");
    var buttonSearch = document.getElementById("button-search2");

    var buttonInsert = document.getElementById("button-insert");
    var buttonDelete = document.getElementById("button-delete");

    var navSignin = document.getElementById("nav-signin");
    var navSignup = document.getElementById("nav-signup");
    var navCustomerTicket = document.getElementById("nav-customer");
    var navUserTicket = document.getElementById("nav-user");
    var navSignout = document.getElementById("nav-signout")

    var pageSignin = document.getElementById("signin");
    var pageCustomerTicket = document.getElementById("ticket-system")
    var pageUserTicket = document.getElementById("page-respond-tickets")
    var pageSignUp = document.getElementById("signuplink")

    var buttonUpdate = document.getElementById("button-update");
    var buttonUpdateTicket = document.getElementById("button-respond");

    var globalUserId = 0;

    var globalCustomerId = 0;



    // var showFormInsert = document.getElementById("form-insert");
    // var showFormUpdate = document.getElementById("form-update");
    // var showFormDelete = document.getElementById("form-delete");
    // var showFormSearch = document.getElementById("form-search");

    buttonSearch.addEventListener("click", searchUsers)
    buttonInsert.addEventListener("click", insertTicket);
    buttonDelete.addEventListener("click", handleButtonDeleteClick);
    buttonUpdate.addEventListener("click", updateTicket);
    buttonUpdateTicket.addEventListener("click", updateTicketUser)

    navSignin.addEventListener("click", handleClickNavSignin);
    navCustomerTicket.addEventListener("click", handleClickNavCustomerTicket);
    navUserTicket.addEventListener("click", handleClickNavUserTicket);
    navSignup.addEventListener("click", handleClickNavSignup);
    navSignout.addEventListener("click", handleClickNavSignin);

    function handleClickNavSignin() {
        // window.history.pushState({}, "", "/" + "employees");
        showPage("sign-in");
        // e.preventDefault();
    }

    function handleClickNavCustomerTicket() {
        // window.history.pushState({}, "", "/" + "departments");
        showPage("customer-ticket");
        // e.preventDefault();
    }

    function handleClickNavSignup() {
        // window.history.pushState({}, "", "/" + "products");
        showPage("sign-up");
        // e.preventDefault();
    }

    function handleClickNavUserTicket() {
        // window.history.pushState({}, "", "/" + "employees");
        showPage("user-ticket");
        // e.preventDefault();
    }

    function showPage(page) {
        if (page.toLowerCase() === "sign-in" || page === "") {
            pageSignin.classList.remove("visually-hidden");
            pageCustomerTicket.classList.add("visually-hidden");
            pageSignUp.classList.add("visually-hidden");
            pageUserTicket.classList.add("visually-hidden");
            navSignout.classList.add("visually-hidden");
            navUserTicket.classList.add("visually-hidden");
            navSignup.classList.remove("visually-hidden");
            navSignin.classList.remove("visually-hidden");
            navCustomerTicket.classList.add("visually-hidden");
        } else if (page.toLowerCase() === "customer-ticket") {
            pageSignin.classList.add("visually-hidden");
            pageCustomerTicket.classList.remove("visually-hidden");
            pageSignUp.classList.add("visually-hidden");
            pageUserTicket.classList.add("visually-hidden");
            navSignup.classList.add("visually-hidden");
            navUserTicket.classList.add("visually-hidden");
            navSignin.classList.add("visually-hidden");
            navCustomerTicket.classList.remove("visually-hidden");
            navSignout.classList.remove("visually-hidden");
        } else if (page.toLowerCase() === "sign-up") {
            pageSignin.classList.add("visually-hidden");
            pageCustomerTicket.classList.add("visually-hidden");
            pageSignUp.classList.remove("visually-hidden");
            pageUserTicket.classList.add("visually-hidden");
            navSignout.classList.add("visually-hidden");
            navUserTicket.classList.add("visually-hidden");
            navSignup.classList.remove("visually-hidden");
            navSignin.classList.remove("visually-hidden");
            navCustomerTicket.classList.add("visually-hidden");
        } else if (page.toLowerCase() === "user-ticket") {
            pageSignin.classList.add("visually-hidden");
            pageCustomerTicket.classList.add("visually-hidden");
            pageSignUp.classList.add("visually-hidden");
            pageUserTicket.classList.remove("visually-hidden");
            navSignup.classList.add("visually-hidden");
            navSignin.classList.add("visually-hidden");
            navUserTicket.classList.remove("visually-hidden");
            navCustomerTicket.classList.add("visually-hidden");
            navSignout.classList.remove("visually-hidden");
        }
    }

    function showDialogBox(message) {
        var dialog = document.getElementById("dialog");
        var dialogMessage = document.getElementById("dialog-message");
        var dialogCloseButton = document.querySelector(".dialog-close-btn");

        dialogMessage.textContent = message;
        dialog.style.display = "flex";

        dialogCloseButton.addEventListener("click", function () {
            dialog.style.display = "none";
        });
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
                        showPage("user-ticket");
                        globalUserId = response.user_id;
                        getTicketsUser();


                    } else {
                        // alert("API Error: " + response.message);
                        buttonSearch.addEventListener("click", searchCustomers());
                    }
                    var response = JSON.parse(xhr.responseText);
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }

    }

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
                        showPage("customer-ticket");
                        globalCustomerId = response.customer_id;
                        getTickets(response.customer_id);

                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }
    }



    function getTickets(globalCustomerId) {

        var url = "http://localhost:5079/GetTickets?customer_id=" + globalCustomerId;

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
        }
    }


    function insertTicket() {

        var textSubject = document.getElementById("text-insert-Subject");
        var textDescription = document.getElementById("text-insert-Description");
        var ePriority = document.getElementById("priority-dropdown");

        var url = "http://localhost:5079/InsertTicket?TicketSubject=" + textSubject.value + "&ticketdescription=" + textDescription.value + "&ticketstatus=open&ticketpriority=" + ePriority.value + "&customerid=" + globalCustomerId;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterInsertTicket;
        xhr.open("GET", url, true);
        xhr.send(null);

        function doAfterInsertTicket() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        getTickets(globalCustomerId);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                    console.log(response);
                }
            }
        }

        var formInsert = document.getElementById("form-insert");
        // formInsert.classList.add("visually-hidden");
        // buttonShowInsertForm.classList.remove("visually-hidden");

        textSubject.value = "";
        textDescription.value = "";
    }

    function updateTicket() {

        var textTicketId = document.getElementById("text-update-ticket-id");
        var textTicketSubject = document.getElementById("text-update-ticket-subject");
        var textTicketDescription = document.getElementById("text-update-ticket-description");

        var url = "http://localhost:5079/updateticket?ticket_id=" + textTicketId.value + "&ticketsubject=" + textTicketSubject.value + "&ticketdescription=" + textTicketDescription.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterUpdateTicket;
        xhr.open("GET", url);
        xhr.send(null);

        function doAfterUpdateTicket(response) {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        getTickets(globalCustomerId);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }

        // var updateForm = document.getElementById("form-update");
        // updateForm.classList.add("visually-hidden");

        // textEmployeeId.value = "";
        // textFirstName.value = "";
        // textLastName.value = "";
        // textSalary.value = "";
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
                        getTickets(globalCustomerId);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }

    }

    function getTicketsUser() {

        var url = "http://localhost:5079/GetAllTickets";

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
                        showTicketsUser(response.tickets);
                        // alert("Hello World")
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        }
    }

    function updateTicketUser() {

        var textTicketId = document.getElementById("text-respond-ticket-id");
        var textUpdateMessage = document.getElementById("text-respond-ticket-response");
        var textUpdateStatus = document.getElementById("status-dropdown");

        var url = "http://localhost:5079/insertupdateticket?user_id=" + globalUserId + "&ticketid=" + textTicketId.value + "&updatecontent=" + textUpdateMessage.value + "&ticket_id=" + textTicketId.value + " &user_id=" + globalUserId + "&ticketstatus=" + textUpdateStatus.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterUpdateTicketUser;
        xhr.open("GET", url);
        xhr.send(null);

        function doAfterUpdateTicketUser() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        getTicketsUser();
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

    function showTicketsUser(tickets) {
        var ticketTableUserText = "<table class='table table-striped table-sm'><thead><tr><th scope='col'>Ticket ID</th><th scope='col'>Subject</th><th scope='col'>Status</th><th scope='col'>Priority</th><th scope='col'></th><th class='button-column'></th></tr></thead><tbody>";

        for (var i = 0; i < tickets.length; i++) {
            var ticket = tickets[i];

            ticketTableUserText += "<tr data-ticket-id='" + ticket.ticket_id + "' data-ticket-description='" + ticket.ticketDescription + "'><th scope='row'>" + ticket.ticket_id + "</th><td id='emp-" + ticket.ticket_id + "-subject'>" + ticket.ticketSubject + "</td><td id='emp-" + ticket.ticket_id + "-status'>" + ticket.ticketStatus + "</td><td id='emp-" + ticket.ticket_id + "-subject'>" + ticket.ticketPriority + "</td><td><div class='row g-2'><div class='col-auto'><button type='button' data-ticket-id='" + ticket.ticket_id + "' class='btn btn-outline-primary btn-sm btn-ticket-table-update'>Update</button></div><div class='col-auto'><button id='' type='button' data-ticket-id='" + ticket.ticket_id + "' class='btn btn-outline-primary btn-sm btn-ticket-table-delete'>Delete</button></div></div></td></tr>";
        }

        ticketTableUserText += "</tbody></table>";

        var ticketTableUser = document.getElementById("response-table");
        ticketTableUser.innerHTML = ticketTableUserText;

        var ticketRows = document.getElementsByTagName("tr");

        for (var i = 0; i < ticketRows.length; i++) {
            var ticketRow = ticketRows[i];

            ticketRow.addEventListener("click", handleTicketTableRowClick);
        }

        async function handleTicketTableRowClick(event) {
            var ticketDescription = event.currentTarget.dataset.ticketDescription;
            var ticketId = event.currentTarget.dataset.ticketId;

            try {
                var response = await fetch("http://localhost:5079/getTicketUpdate?ticket_Id=" + ticketId);
                var data = await response.json();
                var updateContents = data.updates;
            } catch (error) {
                console.log("Error fetching ticket updates:", error);
                var updateContents = [];
            }

            var alertMessage = "Ticket Description: " + ticketDescription + "\n";
            alertMessage += "Ticket Updates:\n";

            if (updateContents && updateContents.length > 0) {
                updateContents.forEach(function (update, index) {
                    alertMessage += "Update " + (index + 1) + ": " + update + "\n";
                });
            } else {
                alertMessage += "No updates available.";
            }

            showDialogBox(alertMessage);
        }



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








}
CIATwebapp();