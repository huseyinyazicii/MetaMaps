var draggables = document.querySelectorAll(".branch");
var containers = document.querySelectorAll(".box");

draggables.forEach(d => {
    d.addEventListener("dragstart", () => {
        d.classList.add("dragging")
    })
})

draggables.forEach(d => {
    d.addEventListener("dragend", () => {
        d.classList.remove("dragging")
    })
})

containers.forEach(c => {
    c.addEventListener("dragover", e => {
        e.preventDefault()
        const afterElement = getDragAfterElement(c, e.clientY)
        const d = document.querySelector('.dragging')
        if (afterElement == null) {
        c.appendChild(d)
        } else {
        c.insertBefore(d, afterElement)
        }
    })
})

function getDragAfterElement(container, y) {
    const draggableElements = [...container.querySelectorAll('.branch:not(.dragging)')]
  
    return draggableElements.reduce((closest, child) => {
      const box = child.getBoundingClientRect()
      const offset = y - box.top - box.height / 2
      if (offset < 0 && offset > closest.offset) {
        return { offset: offset, element: child }
      } else {
        return closest
      }
    }, { offset: Number.NEGATIVE_INFINITY }).element
}

document.querySelector("#gonder").addEventListener("click", function (e) {
    const box2 = document.querySelectorAll(".box2");
    const stepname = document.querySelectorAll("#stepname");

    var steps = new Array();
    var branch = new Array();

    for (let i = 0; i < stepname.length; i++) {
        steps[i] = Number(stepname[i].value);

        for (let j = 0; j < box2[i].childElementCount; j++) {
            branch.push(Number(box2[i].children[j].id));
        }
        branch.push(-1);
    }

    let roadMap = {
        Id: Number($("#roadMapId").val()),
        Steps: steps,
        BranchOfStep: branch
    };

    $.ajax({
        type: "Post",
        url: "/Admin/RoadMap/SecondAddDetail/",
        data: roadMap,
        success: function () {}
    });
    
});

document.querySelector("#ekle").addEventListener("click", function (e) {

    let options = "";

    $.ajax({
        contentType: "application/json",
        dataType: "json",
        type: "Get",
        url: "/Admin/Step/GetAll/",
        success: function (func) {
            let w = jQuery.parseJSON(func);
            for (var i = 0; i < w.length; i++) {
                options += `<option value= ${w[i].Id}>${w[i].Name}</option>`;
            }

            var div = document.createElement("div");
            div.classList.add("step");
            div.classList.add("shadow");
            div.style.borderRadius = "10px";
            div.innerHTML = `<div class="mb-3">
                        <label class="form-label">Adımın İsmi</label>
                        <span onclick="tikla(this)" class="btn btn1 mb-2" style="float: right; padding: 2px 10px 2px 10px;">Sil</span>
                        <select id="stepname" class="form-control">
                            ${options}
                        </select>
                    </div>
                    <div class="box box2"></div>`;

            const steps = document.querySelector("#steps");
            var firstChild = document.querySelector(".step");
            steps.insertBefore(div, firstChild);


            containers = document.querySelectorAll(".box");
            containers.forEach(c => {
                c.addEventListener("dragover", e => {
                    e.preventDefault()
                    const afterElement = getDragAfterElement(c, e.clientY)
                    const d = document.querySelector('.dragging')
                    if (afterElement == null) {
                        c.appendChild(d)
                    } else {
                        c.insertBefore(d, afterElement)
                    }
                })
            })
        }
    })

});

function search() {
    var input, filter, i, txtValue, branches;

    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    branches = document.getElementsByClassName("branch");

    for (i = 0; i < branches.length; i++) {
        txtValue = branches[i].innerText || branches[i].textContent;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            branches[i].style.display = "";
        } else {
            branches[i].style.display = "none";
        }
    }
}

function tikla(thiss) {
    const branchlist = document.getElementById("branchlist");

    var elements = thiss.parentElement.parentElement.lastElementChild;
    var number = elements.childElementCount;

    for (let i = 0; i < number; i++) {
        branchlist.appendChild(elements.children[0]);
    }

    thiss.parentElement.parentElement.remove();
}

function readURL(input) {
    if (input.files && input.files[0]) 
    {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image')
            .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}