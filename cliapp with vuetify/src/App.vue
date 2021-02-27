<template>
  <v-app>
    <v-navigation-drawer
      v-model="drawer"
      :mini-variant="miniVariant"
      :clipped="clipped"
      app
    >
      <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          router
          exact
        >
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar :clipped-left="clipped" app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-btn icon @click.stop="miniVariant = !miniVariant">
        <v-icon>mdi-{{ `chevron-${miniVariant ? "right" : "left"}` }}</v-icon>
      </v-btn>
      <v-btn icon @click.stop="clipped = !clipped">
        <v-icon>mdi-application</v-icon>
      </v-btn>
      <v-toolbar-title v-text="title" />
    </v-app-bar>
    <v-main>
      <v-container>
        
        <router-view></router-view>

        <v-footer app>
          <span>&copy; {{ new Date().getFullYear() }}</span>
        </v-footer>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>

export default {
  data() {
    return {
      clipped: false,
      drawer: false,
      items: [
        {
          icon: "mdi-book-open-blank-variant",
          title: "Form",
          to: "/Form",
        },
        {
          icon: "mdi-table",
          title: "Table",
          to: "/Contacts",
        },
      ],
      miniVariant: false,
      right: true,
      title: "Vue JS By Aleksei Demidenko",
    };
  },
};
</script>

