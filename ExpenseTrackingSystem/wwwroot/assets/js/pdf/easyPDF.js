function easyPDF(_base64, _title) {
  // HTML definition of dialog elements
  //   var dialog =
  //     '<div id="pdfDialog" title="' +
  //     _title +
  //     '">' +
  //     '<label>Page: </label><label id="pageNum"></label><label> of </label><label id="pageLength"></label>' +
  //     '<canvas id="pdfview"></canvas>' +
  //     "</div>";
  //   $("div[id=pdfDialog]").remove();
  //   $(document.body).append(dialog);

  // We need the javascript object of the canvas, not the jQuery reference
  // Init page count

  // Init page number and the document
  $("#pageNum").text(page);
}

function RenderPDF(_base64, pageNumber, canvas_id) {
  var canvas = document.getElementById(canvas_id);
  var pdfData = atob(_base64);
  pdfjsLib.disableWorker = true;

  // Get current global page number, defaults to 1
  displayNum = parseInt(
    $("#" + canvas_id)
      .closest(".card")
      .find("#pageNum")
      .html()
  );
  pageNumber = parseInt(pageNumber);

  var loadingTask = pdfjsLib.getDocument({ data: pdfData });
  loadingTask.promise
    .then(function (pdf) {
      // Gets total page length of pdf
      size = pdf.numPages;
      $("#" + canvas_id)
        .closest(".card")
        .find("#pageLength")
        .text(size);
      // Handling for changing pages
      if (pageNumber == 1) {
        pageNumber = displayNum + 1;
      }
      if (pageNumber == -1) {
        pageNumber = displayNum - 1;
      }
      if (pageNumber == 0) {
        pageNumber = 1;
      }
      // If the requested page is outside the document bounds
      if (pageNumber > size || pageNumber < 1) {
        throw "bad page number";
      }
      // Changes the cheeky global to our valid new page number
      $("#" + canvas_id)
        .closest(".card")
        .find("#pageNum")
        .text(pageNumber);
      pdf.getPage(pageNumber).then(function (page) {
        var scale = 2.0;
        var viewport = page.getViewport(scale);
        var context = canvas.getContext("2d");
        canvas.height = viewport.height;
        canvas.width = viewport.width;
        var renderContext = {
          canvasContext: context,
          viewport: viewport,
        };
        page.render(renderContext);
      });
    })
    .catch((e) => {});
}
