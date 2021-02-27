<template>
  <div :class="{ Sending: isSending }">
    <form>
      <v-text-field
        v-model="formData.name"
        :error-messages="nameErrors"
        :counter="10"
        label="Name"
        required
        @input="$v.formData.name.$touch()"
        @blur="$v.formData.name.$touch()"
      ></v-text-field>
      <v-text-field
        v-model="formData.middleName"
        label="Middle name"
        disabled
      ></v-text-field>
      <v-text-field
        v-model="formData.lastName"
        :error-messages="lastNameErrors"
        label="Last name"
        required
        @input="$v.formData.lastName.$touch()"
        @blur="$v.formData.lastName.$touch()"
      ></v-text-field>
      <v-text-field
        v-model="formData.email"
        :error-messages="emailErrors"
        label="E-mail"
        placeholder="Example@gmail.com"
        required
        @input="$v.formData.email.$touch()"
        @blur="$v.formData.email.$touch()"
      ></v-text-field>
      <v-text-field
        v-model="formData.phone"
        :error-messages="phoneErrors"
        label="Phone"
        placeholder="+7 (999) 999 99-99"
        required
        @input="$v.formData.phone.$touch()"
        @blur="$v.formData.phone.$touch()"
      ></v-text-field>
      <v-menu
        ref="dateMenu"
        v-model="dateMenu"
        :close-on-content-click="false"
        transition="scale-transition"
        offset-y
        min-width="auto"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="formData.dateOfBirthday"
            label="Birthday date"
            prepend-icon="mdi-calendar"
            readonly
            v-bind="attrs"
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker
          ref="picker"
          v-model="formData.dateOfBirthday"
          :max="new Date().toISOString().substr(0, 10)"
          min="1950-01-01"
          @change="saveDate"
        ></v-date-picker>
      </v-menu>
      <v-textarea label="Message" v-model="formData.message"></v-textarea>
      <v-file-input
        v-model="formData.avatar"
        accept="image/*"
        label="Avatar"
        :error-messages="avatarErrors"
        @input="$v.formData.avatar.$touch()"
        @blur="$v.formData.avatar.$touch()"
      ></v-file-input>

      <v-btn class="mr-4" @click="submit" :loading="isSending">
        submit
      </v-btn>
      <v-btn @click="clear">
        clear
      </v-btn>
    </form>

    <v-dialog v-model="dialog" width="400">
      <v-card>
        <v-card-title class="headline grey lighten-2">
          {{ dialogTitle }}
        </v-card-title>

        <v-card-text>
          {{ dialogText }}
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { contactAPI } from "@/api/api.js";
import { validationMixin } from "vuelidate";
import { required, maxLength, email } from "vuelidate/lib/validators";

const isPhone = (value) =>
  /(\+7|8)[- _]*\(?[- _]*(\d{3}[- _]*\)?([- _]*\d){7}|\d\d[- _]*\d\d[- _]*\)?([- _]*\d){6})/g.test(
    value
  );

const isImage = (value) => {
  if (value && (value.type == "image/jpeg" || value.type == "image/png")) {
    return true;
  } else if (!value) {
    return true;
  } else {
    return false;
  }
};

export default {
  mixins: [validationMixin],

  validations: {
    formData: {
      name: { required, maxLength: maxLength(10) },
      lastName: { required },
      phone: { required, phoneValid: isPhone },
      email: { required, email },
      avatar: { fileTypeValid: isImage },
    },
  },

  data() {
    return {
      dialog: false,
      dialogTitle: null,
      dialogText: null,
      isSending: false,

      dateMenu: false,
      formData: {
        name: null,
        lastName: null,
        middleName: null,
        dateOfBirthday: null,
        phone: null,
        email: null,
        message: null,
        avatar: null,
      },
    };
  },

  computed: {
    nameErrors() {
      const errors = [];
      if (!this.$v.formData.name.$dirty) return errors;
      !this.$v.formData.name.maxLength &&
        errors.push("Name must be at most 10 characters long");
      !this.$v.formData.name.required && errors.push("Name is required.");
      return errors;
    },
    lastNameErrors() {
      const errors = [];
      if (!this.$v.formData.lastName.$dirty) return errors;
      !this.$v.formData.lastName.required && errors.push("Last name is required");
      return errors;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.formData.email.$dirty) return errors;
      !this.$v.formData.email.email && errors.push("Must be valid e-mail");
      !this.$v.formData.email.required && errors.push("E-mail is required");
      return errors;
    },
    phoneErrors() {
      const errors = [];
      if (!this.$v.formData.phone.$dirty) return errors;
      !this.$v.formData.phone.phoneValid && errors.push("Must be valid phone");
      !this.$v.formData.phone.required && errors.push("Phone is required");
      return errors;
    },
    avatarErrors() {
      const errors = [];
      if (!this.$v.formData.avatar.$dirty) return errors;
      !this.$v.formData.avatar.fileTypeValid &&
        errors.push("The file must be jpg or png");
      return errors;
    },
  },
  methods: {
    async submit() {
      this.$v.$touch();
      if (this.$v.formData.$invalid) return;

      const formData = new FormData();
      for (let key in this.formData) {
        formData.append(key, this.formData[key]);
      }

      try {
        this.isSending = true;
        await contactAPI.addContact(formData);
      } catch (e) {
        this.showDialog(
          "Ошибка",
          e.response ? e.response.data.detail : e.message
        );
        return;
      }

      this.showDialog();
      this.clear();
    },
    clear() {
      this.$v.$reset();
      for (let key in this.formData) {
        this.formData[key] = null;
      }
    },
    showDialog(title = "Выполнено", text = "Контакт успешно создан") {
      this.isSending = false;
      this.dialog = true;
      this.dialogTitle = title;
      this.dialogText = text;
    },
    saveDate(date) {
      this.$refs.dateMenu.save(date);
    },
  },
  watch: {
    dateMenu(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = "YEAR"));
    },
    dialog(val) {
      val && setTimeout(() => (this.dialog = false), 3000);
    },
  },
};
</script>
