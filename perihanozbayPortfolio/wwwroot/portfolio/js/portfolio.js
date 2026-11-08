 function updateCharCount(textarea) {
            const currentLength = textarea.value.length;
            const counter = document.getElementById("charCount");
            if (counter) {
              counter.textContent = currentLength + " / 1000";
            }
          }

          function handleFormSubmit() {
            const btn = document.getElementById("submitBtn");
            const alert = document.getElementById("formSuccessAlert");
            const form = document.getElementById("contactForm");

            if (btn) {
              btn.disabled = true;
              btn.innerHTML =
                '<span>Gönderiliyor...</span><span class="material-symbols-outlined text-[18px] animate-spin">refresh</span>';
            }

            setTimeout(() => {
              if (alert) alert.classList.remove("hidden");
              if (btn) {
                btn.innerHTML =
                  '<span>İletildi</span><span class="material-symbols-outlined text-[18px]">done</span>';
                btn.classList.remove("bg-tertiary-container");
                btn.classList.add("bg-secondary");
              }
              if (form) form.reset();
              const counter = document.getElementById("charCount");
              if (counter) counter.textContent = "0 / 1000";
            }, 700);
          }