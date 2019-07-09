(function () {
    var calendarView,
        calendar,
        currentMonthShort;


    // Data
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    var events = [
        {
            id: 1,
            title: 'Prueba',
            start: new Date(y, m, 1),
            end: null
        }

    ];

    $('#calendar-view').fullCalendar({
        events: events,
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
        dayNamesShort: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
        editable: true,
        eventLimit: true,
        header: '',
        handleWindowResize: false,
        aspectRatio: 1,
        viewRender: function (view) {
            console.log(view);
            calendarView = view;
            calendar = view.calendar;

            $('#calendar-view-title').text(view.title);

            $('#calendar > .page-header').removeClass(currentMonthShort);
            currentMonthShort = calendar.getDate().format('MMM');
            $('#calendar > .page-header').addClass(currentMonthShort);

        },
        // columnFormat      : {
        //     month: 'ddd',
        //     week : 'ddd D',
        //     day  : 'ddd M'
        // },
        eventClick: eventClick,
        selectable: true,
        selectHelper: true,
        dayClick: dayClick

    });

    /**
     * Show event detail
     */
    function eventClick(calEvent, jsEvent, view) {

        alert('Event: ' + calEvent.title);
        // alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
        // alert('View: ' + view.name);

        // change the border color just for fun
        // $(this).css('border-color', 'red');
    }

    /**
     * Add new event in between selected dates
     */
    function dayClick(date, jsEvent, view) {

        alert('Clicked on: ' + date.format());

        // alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);

        // alert('Current view: ' + view.name);

        // change the day's background color just for fun
        // $(this).css('background-color', 'red');
    }

    $('#calendar-next-button').click(function () {
        calendar.next();
    });

    $('#calendar-previous-button').click(function () {
        calendar.prev();
    });


    $('#calendar-today-button').click(function () {
        calendar.today();
    });

    $('#calendar .page-header .change-view').click(function () {
        calendar.changeView($(this).data('view'));
    });

})();