// This script manages url input, requests to the Words API
// and the generation of the word cloud.

$(function () {

    // Initialise the word cloud with an empty array.
    $('#word-cloud').jQCloud([], {
        width: 500,
        height: 350
    });

    // When Create Cloud button is clicked, get the value
    // in the input box and send it to the API.
    $('#url-submit').on('click', function () {
        var url = $('#url-input').val();

        if (!url || !url.length)
        {
            $('#message').text('No url was provided. Please try again');
        }
        else
        {
            $.ajax({
                url: '/api/v1/words?url=' + url,
            }).done(function (words) {
                $('#word-cloud').jQCloud('update', words);
            }).fail(function (xmlHttpRequest, statusText, errorThrown) {
                console.log(
                  'Words request error.\n\n'
                    + 'XML Http Request: ' + JSON.stringify(xmlHttpRequest)
                    + ',\nStatus Text: ' + statusText
                    + ',\nError Thrown: ' + errorThrown);
            });
        }
    });
});