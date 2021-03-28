<template>
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
        :value="datePickerValue | dateRU"
        label="Birthday date"
        prepend-icon="mdi-calendar"
        readonly
        v-bind="attrs"
        v-on="on"
      ></v-text-field>
    </template>
    <v-date-picker
      ref="picker"
      v-model="datePickerValue"
      :max="new Date().toISOString().substr(0, 10)"
      min="1950-01-01"
      @change="saveDate"
    ></v-date-picker>
  </v-menu>
</template>

<script>
export default {
  data() {
    return {
      dateMenu: false,
    };
  },
  props: {
    value: {
      type: String,
      default: null,
    },
  },
  methods: {
    saveDate(date) {
      this.$refs.dateMenu.save(date);
    },
  },
  model: {
    prop: "value",
    event: "change",
  },
  computed: {
    datePickerValue: {
      get() {
        return this.value.substring(0, 10);
      },
      set(val) {
        this.$emit("change", val);
      },
    },
  },
  watch: {
    dateMenu(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = "YEAR"));
    },
  },
};
</script>
