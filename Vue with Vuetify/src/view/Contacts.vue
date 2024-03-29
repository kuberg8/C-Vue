<template>
  <v-card>
    <v-card-title>
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
        @input="getContact"
      ></v-text-field>
    </v-card-title>
    <v-data-table
      :headers="headers"
      :items="contacts"
      :search="search"
      :loading="loadingContacts"
      :options.sync="options"
      :server-items-length="totalItems"
    >
      <template v-slot:[`item.phone`]="{ item }">
        <div>
          {{ item.phone | phone }}
        </div>
      </template>
      <template v-slot:[`item.dateOfBirthday`]="{ item }">
        <div>
          {{ item.dateOfBirthday | dateRU }}
        </div>
      </template>      
      <template v-slot:[`item.actions`]="{ item }">
        <div>
          <v-btn
            class="mx-2"
            icon
            @click.stop="(editDialog = true), (editUser = { ...item })"
            title="Редактировать"
          >
            <v-icon dark> mdi-account-edit </v-icon>
          </v-btn>
          <v-btn
            class="mx-2"
            icon
            @click.stop="removeItem(item)"
            title="Удалить"
            :loading="item.deleteLoading"
          >
            <v-icon dark> mdi-delete </v-icon>
          </v-btn>
          <v-btn
            v-if="item.fileId"
            class="mx-2"
            icon
            @click.stop="downloadAvatar(item.fileId)"
            title="Скачать аватарку"
          >
            <v-icon dark> mdi-content-save </v-icon>
          </v-btn>
        </div>
      </template>
    </v-data-table>

    <v-dialog v-model="editDialog" persistent max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">User Profile</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="editUser.name"
                  label="First name"
                  required
                  :error-messages="nameErrors"
                  @input="$v.editUser.name.$touch()"
                  @blur="$v.editUser.name.$touch()"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="editUser.middleName"
                  label="Middle name"
                  :error-messages="middleNameErrors"
                  @input="$v.editUser.middleName.$touch()"
                  @blur="$v.editUser.middleName.$touch()"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="editUser.lastName"
                  label="Last name"
                  :error-messages="lastNameErrors"
                  @input="$v.editUser.lastName.$touch()"
                  @blur="$v.editUser.lastName.$touch()"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="editUser.email"
                  label="Email"
                  :error-messages="emailErrors"
                  @input="$v.editUser.email.$touch()"
                  @blur="$v.editUser.email.$touch()"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  v-model="muskPhone"
                  label="Phone"
                  :maxlength="18"
                  :error-messages="phoneErrors"
                  @input="$v.editUser.phone.$touch()"
                  @blur="$v.editUser.phone.$touch()"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <DatePicker v-model="editUser.dateOfBirthday" />
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="blue darken-1"
            text
            @click="(editDialog = false), $v.$reset()"
          >
            Close
          </v-btn>
          <v-btn
            color="blue darken-1"
            text
            @click="updateUser"
            :loading="editLoading"
          >
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<script>
import Vue from "vue";
import { contactAPI } from "@/api/api.js";
import { validationMixin } from "vuelidate";
import { required, maxLength, email, numeric } from "vuelidate/lib/validators";
import DatePicker from "@/components/DatePicker.vue";

export default {
  mixins: [validationMixin],
  validations: {
    editUser: {
      name: { required, maxLength: maxLength(10) },
      lastName: { required },
      middleName: { required },
      phone: { required: (val) => val != 0, numeric },
      email: { required, email },
    },
  },
  components: {
    DatePicker,
  },
  data() {
    return {
      search: "",
      headers: [
        { text: "Name", value: "name" },
        { text: "Last name", value: "lastName" },
        { text: "Middle Name", value: "middleName" },
        { text: "Phone", sortable: false, value: "phone" },
        { text: "Date of birth", value: "dateOfBirthday" },
        { text: "E-mail", value: "email" },
        { text: "Actions", sortable: false, value: "actions" },
      ],
      options: {
        itemsPerPage: 5,
        page: 1,
        sortBy: ["Name"],
        sortDesc: [false],
      },
      totalItems: 0,
      contacts: [],
      loadingContacts: false,
      editDialog: false,
      editLoading: false,
      editUser: {},
      dateMenu: false,
    };
  },
  mounted() {
    this.getContact();
  },
  methods: {
    getContact() {
      this.loadingContacts = true;
      contactAPI
        .getContact(
          this.options.sortBy[0],
          this.options.sortDesc[0],
          this.options.page,
          this.options.itemsPerPage == -1
            ? this.totalItems
            : this.options.itemsPerPage,
          this.search
        )
        .then((response) => {
          this.contacts = response.data.data;
          this.totalItems = response.data.total_count;
        })
        .catch(() => {})
        .finally(() => {
          this.loadingContacts = false;
        });
    },
    removeItem(item) {
      this.$set(item, "deleteLoading", true); // добавляет реактивное свойство у объекта после его инициализации, можно использовать Vue.set()

      contactAPI
        .deleteContact(item.id)
        .then(() => {
          this.getContact();
        })
        .catch(() => {
          this.$delete(item, "deleteLoading"); // можно использовать Vue.delete()
        });
    },
    downloadAvatar(id) {
      location.href = `https://localhost:44306/Home/Download?FileId=${id}`;
    },
    updateUser() {
      this.$v.$touch();
      if (this.$v.editUser.$invalid) return;
      this.editLoading = true;
      contactAPI
        .updateContact(this.editUser)
        .then(() => {
          this.getContact();
        })
        .finally(() => {
          this.editLoading = false;
          this.editDialog = false;
        });
    },
  },
  computed: {
    muskPhone: {
      get: function() {
        return Vue.filter("phone")(this.editUser.phone);
      },
      set: function(value) {
        this.editUser.phone = Vue.filter("toNumber")(value);
      },
    },
    nameErrors() {
      const errors = [];
      if (!this.$v.editUser.name.$dirty) return errors;
      !this.$v.editUser.name.maxLength &&
        errors.push("Name must be at most 10 characters long");
      !this.$v.editUser.name.required && errors.push("Name is required.");
      return errors;
    },
    middleNameErrors() {
      const errors = [];
      if (!this.$v.editUser.middleName.$dirty) return errors;
      !this.$v.editUser.middleName.required &&
        errors.push("Middle name is required.");
      return errors;
    },
    lastNameErrors() {
      const errors = [];
      if (!this.$v.editUser.lastName.$dirty) return errors;
      !this.$v.editUser.lastName.required &&
        errors.push("Last name is required");
      return errors;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.editUser.email.$dirty) return errors;
      !this.$v.editUser.email.email && errors.push("Must be valid e-mail");
      !this.$v.editUser.email.required && errors.push("E-mail is required");
      return errors;
    },
    phoneErrors() {
      const errors = [];
      if (!this.$v.editUser.phone.$dirty) return errors;
      !this.$v.editUser.phone.required && errors.push("Phone is required");
      !this.$v.editUser.phone.numeric && errors.push("Must be valid phone");
      return errors;
    },
  },
  watch: {
    options: {
      handler() {
        this.getContact();
      },
      deep: true,
    },
  },
};
</script>
