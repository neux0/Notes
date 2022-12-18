fetch("http://localhost:5255/Notes/GetAllNotes")
.then(response => response.json())//parse json data
.then(function(response){
  var notesList = "";
  var notCount = 0;

  response.forEach(element => {
      if (element.visible == true){
          var d = new Date(element.date);
          var date = d.getDate() + "." + (Number(d.getMonth()) + 1 ) + "." + d.getFullYear() + " - " + d.getHours() + "." + d.getMinutes();
          notesList += `
          <div class="note">
              <div class="title">`+ element.title +`</div>
              <div class="content">`+ element.content +`</div>
              <div class="date">Date: `+ date +`</div>
          </div>          
          `
          notCount += 1;
          }
  });
  document.getElementById("notes").innerHTML = notesList;
  document.getElementById("totalNotes").innerText = notCount;
});