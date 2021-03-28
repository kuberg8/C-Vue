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
        :error-messages="middleNameErrors"
        @input="$v.formData.middleName.$touch()"
        @blur="$v.formData.middleName.$touch()"
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
        v-model="muskPhone"
        :error-messages="phoneErrors"
        label="Phone"
        placeholder="+7 (999) 999 99-99"
        required
        :maxlength="18"
        @input="$v.formData.phone.$touch()"
        @blur="$v.formData.phone.$touch()"
      ></v-text-field>
      <DatePicker v-model="formData.dateOfBirthday" />
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
import Vue from "vue";
import { contactAPI } from "@/api/api.js";
import { validationMixin } from "vuelidate";
import { required, maxLength, email, numeric } from "vuelidate/lib/validators";
import DatePicker from "@/components/DatePicker.vue";

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
      middleName: { required },
      lastName: { required },
      phone: { required: (val) => val != 0, numeric },
      email: { required, email },
      avatar: { fileTypeValid: isImage },
    },
  },
  components: {
    DatePicker,
  },
  data() {
    return {
      dialog: false,
      dialogTitle: null,
      dialogText: null,
      isSending: false,
      formData: {
        name: null,
        lastName: null,
        middleName: null,
        dateOfBirthday: new Date().toISOString(),
        phone: 0,
        email: null,
        message: null,
        avatar: null,
      },
    };
  },

  computed: {
    muskPhone: {
      get: function() {
        return Vue.filter("phone")(this.formData.phone);
      },
      set: function(value) {
        this.formData.phone = Vue.filter("toNumber")(value);
      },
    },
    nameErrors() {
      const errors = [];
      if (!this.$v.formData.name.$dirty) return errors;
      !this.$v.formData.name.maxLength &&
        errors.push("Name must be at most 10 characters long");
      !this.$v.formData.name.required && errors.push("Name is required.");
      return errors;
    },
    middleNameErrors() {
      const errors = [];
      if (!this.$v.formData.middleName.$dirty) return errors;
      !this.$v.formData.middleName.required &&
        errors.push("Middle name is required.");
      return errors;
    },
    lastNameErrors() {
      const errors = [];
      if (!this.$v.formData.lastName.$dirty) return errors;
      !this.$v.formData.lastName.required &&
        errors.push("Last name is required");
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
      !this.$v.formData.phone.numeric && errors.push("Must be valid phone");
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
  },
  watch: {
    dialog(val) {
      val && setTimeout(() => (this.dialog = false), 3000);
    },
  },
};
</script>
