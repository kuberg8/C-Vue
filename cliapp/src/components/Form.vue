<template>
  <div :class="{ Sending: isSending }">
    <form @submit.prevent="addClient">
      <div class="block">
        <h1>Анкета</h1>
        <span class="label"
          >Фамилия
          <b>*</b>
          <span
            v-show="$v.formData.surname.$dirty && !$v.formData.surname.required"
            class="required"
            >Поле обязательно для заполнения</span
          >
        </span>
        <input
          name="surname"
          placeholder="Введите вашу фамилию"
          v-model.trim="formData.surname"
          :class="{
            errorClass:
              $v.formData.surname.$dirty && !$v.formData.surname.required,
          }"
        />

        <span class="label"
          >Имя
          <b>*</b>
          <span
            v-show="$v.formData.name.$dirty && !$v.formData.name.required"
            class="required"
            >Поле обязательно для заполнения</span
          ></span
        >
        <input
          name="name"
          placeholder="Введите ваше имя"
          v-model.trim="formData.name"
          :class="{
            errorClass: $v.formData.name.$dirty && !$v.formData.name.required,
          }"
        />

        <span class="label space">
          <span
            >Отчество
            <b v-show="!haveNotMiddleName">*</b>
            <span
              v-show="error && formData.middleName === '' && !haveNotMiddleName"
              class="required"
              >Поле обязательно для заполнения</span
            >
          </span>
          <span style="white-space: nowrap; width: 150px;">
            <input
              id="middleName"
              type="checkbox"
              name="haveNotMiddleName"
              v-model="haveNotMiddleName"
              style="height: 20px; margin: 0;"
            />
            <label for="middleName">Нет отчества</label>
          </span>
        </span>
        <input
          name="middleName"
          placeholder="Введите ваше отчество"
          v-model="formData.middleName"
          :class="{
            errorClass:
              error && formData.middleName === '' && !haveNotMiddleName,
          }"
          :disabled="haveNotMiddleName ? true : false"
        />

        <span class="label"
          >Дата рождения
          <b>*</b>
          <span
            v-show="
              $v.formData.dateOfBirthday.$dirty &&
                !$v.formData.dateOfBirthday.required
            "
            class="required"
            >Поле обязательно для заполнения</span
          >
        </span>
        <input
          type="date"
          name="dateOfBirthday"
          v-model="formData.dateOfBirthday"
          :class="{
            errorClass:
              $v.formData.dateOfBirthday.$dirty &&
              !$v.formData.dateOfBirthday.required,
          }"
        />

        <span class="label"
          >Тип документа
          <b>*</b>
          <span
            v-show="
              $v.formData.document.$dirty && !$v.formData.document.required
            "
            class="required"
            >Поле обязательно для заполнения</span
          >
        </span>
        <select
          name="document"
          v-model="formData.document"
          :class="{
            errorClass:
              $v.formData.document.$dirty && !$v.formData.document.required,
          }"
        >
          <option value="Паспорт" selected>Паспорт</option>
          <option value="Свидетельство о рождении"
            >Свидетельство о рождении</option
          >
          <option value="Водительское удостоверение"
            >Водительское удостоверение</option
          >
        </select>

        <span class="block_document" v-show="formData.document === 'Паспорт'">
          <span class="label"
            >Серия
            <b>*</b>
          </span>
          <input
            name="series"
            type="number"
            placeholder="Введите серию паспорта"
            v-model.number="formData.series"
            :class="{ errorClass: error && formData.series === '' }"
          />

          <span class="label">Номер<b>*</b></span>
          <input
            name="pasportNumber"
            type="number"
            placeholder="Введите номер паспорта"
            v-model.number="formData.pasportNumber"
            :class="{ errorClass: error && formData.pasportNumber === '' }"
          />

          <span class="label">Дата выдачи<b>*</b></span>
          <input
            type="date"
            name="dateOfIssue"
            placeholder="Введите дату выдачи паспорта"
            v-model.trim="formData.dateOfIssue"
            :class="{ errorClass: error && formData.dateOfIssue === '' }"
          />

          <span class="label">Кем выдан<b>*</b></span>
          <input
            name="issuedBy"
            placeholder="Введите кем выдан паспорт"
            v-model.trim="formData.issuedBy"
            :class="{ errorClass: error && formData.issuedBy === '' }"
          />
        </span>

        <span
          class="block_document"
          v-show="formData.document === 'Свидетельство о рождении'"
        >
          <span class="label">Свидетельство о рождении<b>*</b></span>
          <input
            name="birthCertificate"
            type="number"
            placeholder="Введите номер свидетельства"
            v-model.number="formData.birthCertificate"
            :class="{ errorClass: error && formData.birthCertificate === '' }"
          />
        </span>

        <span
          class="block_document"
          v-show="formData.document === 'Водительское удостоверение'"
        >
          <span class="label"
            >Номер водительского удостоверения
            <b>*</b>
          </span>
          <input
            name="driveNumber"
            type="number"
            placeholder="Введите номер водительского"
            v-model.number="formData.driveNumber"
            :class="{ errorClass: error && formData.driveNumber === '' }"
          />
        </span>

        <span class="label"
          >Контактный телефон
          <b>*</b>
          <span
            v-show="$v.formData.phone.$dirty && !$v.formData.phone.required"
            class="required"
            >Поле обязательно для заполнения</span
          >
          <span
            v-show="
              $v.formData.phone.$dirty &&
                !$v.formData.phone.phoneValid &&
                formData.phone != ''
            "
            class="required"
            >Пример: +7 999 666 22 22</span
          >
        </span>
        <input
          name="phone"
          type="tel"
          placeholder="Введите номер телефона"
          v-model="formData.phone"
          :class="{
            errorClass:
              ($v.formData.phone.$dirty && !$v.formData.phone.required) ||
              ($v.formData.phone.$dirty && !$v.formData.phone.phoneValid),
          }"
        />

        <span class="label"
          >Email
          <b>*</b>
          <span
            v-show="$v.formData.Email.$dirty && !$v.formData.Email.required"
            class="required"
            >Поле обязательно для заполнения</span
          >
          <span v-show="!$v.formData.Email.email" class="required"
            >Пример: example@site.com</span
          >
        </span>
        <input
          name="Email"
          type="email"
          placeholder="Введите Email"
          v-model="formData.Email"
          :class="{
            errorClass:
              ($v.formData.Email.$dirty && !$v.formData.Email.required) ||
              !$v.formData.Email.email,
          }"
        />

        <span class="label"
          >Сообщение
          <b>*</b>
          <span
            v-show="$v.formData.Message.$dirty && !$v.formData.Message.required"
            class="required"
            style="bottom: -25px; right: -10px;"
            >Поле обязательно для заполнения</span
          >
        </span>
        <textarea
          name="Message"
          placeholder="Напишите ваше обращение.."
          v-model="formData.Message"
          :class="{
            errorClass:
              $v.formData.Message.$dirty && !$v.formData.Message.required,
          }"
        >
        </textarea>

        <span class="label"
          >Фото
          <span v-show="errorFileType" class="required"
            >Формат jpg или png</span
          >
        </span>
        <input
          name="Avatar"
          type="file"
          ref="file"
          accept=".jpg,.png"
          :class="{ errorClass: errorFileType }"
        />
        <!-- or accept="image/*" -->
      </div>

      <button type="submit" :disabled="isSending">Создать</button>
    </form>

    <div v-show="modal" class="modal">
      <div class="modal_window">
        <span>{{ response }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { contactAPI } from "../api/api.js";
import { required, email } from "vuelidate/lib/validators";

const isPhone = (value) =>
  /(\+7|8)[- _]*\(?[- _]*(\d{3}[- _]*\)?([- _]*\d){7}|\d\d[- _]*\d\d[- _]*\)?([- _]*\d){6})/g.test(
    value
  );

export default {
  data() {
    return {
      haveNotMiddleName: false,
      error: false,
      modal: false,
      response: null,
      isSending: false,
      errorFileType: false,

      formData: {
        surname: "",
        name: "",
        middleName: "",
        dateOfBirthday: "",

        document: "",

        series: "",
        pasportNumber: "",
        issuedBy: "",
        dateOfIssue: "",

        birthCertificate: "",
        driveNumber: "",

        phone: "",
        Email: "",
        Message: "",
      },
    };
  },
  validations: {
    formData: {
      surname: { required },
      name: { required },
      dateOfBirthday: { required },
      phone: { required, phoneValid: isPhone },
      document: { required },
      Email: { required, email },
      Message: { required },
    },
  },
  methods: {
    async addClient() {
      let form = this.formData;
      let file = this.$refs.file.files[0];

      if (this.$v.formData.$invalid) {
        this.$v.$touch();
        form.middleName === "" && !form.haveNotMiddleName
          ? (this.error = true)
          : null;

        if (file && file.type != "image/jpeg" && file.type != "image/png") {
          this.errorFileType = true;
          return;
        }
        this.errorFileType = false;
        return;
      } else if (form.middleName === "" && !this.haveNotMiddleName) {
        this.error = true;
        return;
      }

      switch (form.document) {
        case "Паспорт":
          if (
            form.series === "" ||
            form.pasportNumber === "" ||
            form.issuedBy === "" ||
            form.dateOfIssue === ""
          ) {
            this.error = true;
            return;
          }
          break;
        case "Свидетельство о рождении":
          if (form.birthCertificate === "") {
            this.error = true;
            return;
          }
          break;
        case "Водительское удостоверение":
          if (form.driveNumber === "") {
            this.error = true;
            return;
          }
          break;
      }

      let obj = {
        Name: this.formData.name,
        LastName: this.formData.surname,
        MiddleName: this.formData.middleName,
        DateOfBirthday: this.formData.dateOfBirthday,
        Email: this.formData.Email,
        Phone: this.formData.phone,
        DocumentType: this.formData.document,
        Series: +this.formData.series,
        PasportNumber: +this.formData.pasportNumber,
        IssuedBy: this.formData.issuedBy,
        DateOfIssue: this.formData.dateOfIssue,
        BirthCertificate: +this.formData.birthCertificate,
        DriveNumber: +this.formData.driveNumber,
        Message: this.formData.Message,
      };

      const formData = new FormData();
      for (let key in obj) {
        formData.append(key, obj[key]);
      }
      formData.append("Avatar", this.$refs.file.files[0]);

      try {
        this.isSending = true;
        await contactAPI.addContact(formData);
      } catch (e) {
        this.modalWindow(e.response ? e.response.data.detail : "Ошибка");
        return;
      }

      this.modalWindow("Контакт успешно создан!");

      for (let key in this.formData) {
        this.formData[key] = "";
      }

      this.$refs.file.value = "";
      this.error = false;
      this.$v.$reset();
    },
    modalWindow(message) {
      this.modal = true;
      this.response = message;
      setTimeout(() => (this.modal = false), 2000);
      this.isSending = false;
    },
  },
};
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;
}

input {
  width: 100%;
  height: 30px;
  font-size: 20px;
  padding: 10px;
  outline: none;
  background: #1c1e21;
  color: white;
  border-radius: 10px;
  border: 2px solid #34383d;
  transition: border 0.2s, background 0.2s linear;
  margin-bottom: 20px;
}
input[type="number"] {
  -moz-appearance: textfield;
}
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
}

input:disabled {
  background: #ccc;
}

textarea {
  max-width: 100%;
  min-width: 100%;
  min-height: 120px;
  font-size: 20px;
  padding: 10px;
  outline: none;
  background: #1c1e21;
  color: white;
  border-radius: 10px;
  border: 2px solid #34383d;
  transition: border 0.2s, background 0.2s linear;
  margin-bottom: 20px;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

form span {
  display: flex;
  align-items: center;
  font-size: 20px;
}

select {
  width: 100%;
  height: 50px;
  font-size: 20px;
  outline: none;
  background: #1c1e21;
  color: white;
  border-radius: 10px;
  border: 2px solid #34383d;
  transition: border 0.2s, background 0.2s linear;
  margin: 0 auto 20px;
}

button {
  display: block;
  height: 40px;
  border-radius: 5px;
  border: none;
  background: цршеу;
  margin: 0 auto;
  font-size: 20px;
  font-weight: bold;
  outline: none;
  cursor: pointer;
  padding: 5px 10px;
}

button:hover {
  opacity: 0.9;
}

b {
  color: red;
  margin-left: 5px;
}

.errorClass {
  border: 2px solid red;
  background: #ebd2dd;
  color: black;
}

.Sending {
  cursor: progress;
}
.Sending input {
  cursor: progress;
}
.Sending textarea {
  cursor: progress;
}
.Sending button {
  cursor: progress;
}

.space {
  justify-content: space-between;
  align-items: center;
}

.label {
  position: relative;
}

.required {
  position: absolute;
  color: red;
  right: 30px;
  bottom: -50px;
  white-space: nowrap;
}
</style>
