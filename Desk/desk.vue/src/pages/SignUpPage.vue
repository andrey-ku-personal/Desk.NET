<script lang="ts">
import { defineComponent } from "vue";

import { BaseHttp } from "@/helpers/BaseHttp";
import { CreateRequest } from "@/components/user/requests/createRequest";

import useVuelidate from "@vuelidate/core";
import { required, email } from "@vuelidate/validators";

export default defineComponent({
  name: "sign-up-page",

  setup() {
    return { v$: useVuelidate() };
  },

  data: function () {
    return {
      request: {
        id: 0,
        firstName: "",
        lastName: "",
        email: "",
        password: "",
      } as CreateRequest,
    };
  },

  validations() {
    return {
      request: {
        id: 0,
        firstName: { required },
        lastName: { required },
        email: { required, email },
        password: { required },
      },
    };
  },

  methods: {
    async signUp(): Promise<void> {
      const isFormCorrect = await this.v$.$validate();

      if (isFormCorrect) {
        try {
          const response = await BaseHttp.post("User/Create", this.request);

          if (response.status === 200) {
            console.log(response.data);
          }
        } catch (error) {
          console.log(error);
        }
      }
    },
  },
});
</script>

<template>
  <header>
    <img
      class="header-logo"
      src="@/assets/images/logo.svg"
      width="50"
      height="50"
    />
    <div class="header-title"><h2>Task Desk</h2></div>
  </header>
  <div class="container">
    <div class="content">
      <div class="other-service">
        <h1 class="title">Get Started</h1>
        <h4 class="service-subtitle title">with one of these services</h4>
        <div class="other-service-button">
          <p-button
            label="Sign up with Google"
            class="p-button-outlined p-button-warning"
            icon="pi pi-google"
          />
          <p-button
            label="Sign up with Facebook"
            class="p-button-outlined p-button-info"
            icon="pi pi-facebook"
          />
        </div>
      </div>
      <div class="separator">
        <div class="vertical-line"></div>
        <p class="or">Or</p>
      </div>
      <div class="custom-service">
        <h1 class="title">Get Started</h1>
        <h4 class="service-subtitle title">with your email address</h4>
        <form class="form" @submit.prevent>
          <input-text
            label="First Name*:"
            v-model.trim="request.firstName"
            placeholder="Write First Name"
            :validation="v$.request.firstName"
          />
          <input-text
            label="Last Name*:"
            v-model.trim="request.lastName"
            placeholder="Write First Name"
            :validation="v$.request.lastName"
          />
          <input-text
            label="Email*:"
            v-model.trim="request.email"
            placeholder="Write Email"
            :validation="v$.request.email"
          />
          <input-password
            label="Password*:"
            v-model:password="request.password"
            placeholder="Write Password"
            :validation="v$.request.password"
          />

          <div class="form-buttons">
            <p-button class="p-button-raised" label="Sign Up" @click="signUp" />
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
header {
  width: 100%;
  padding: 20px;

  display: flex;
}

header .header-title {
  line-height: 50px;
  margin-left: 15px;
}

header .header-title h2 {
  margin: 0;
}

.conteiner {
  display: flex;
  justify-content: center;
  align-items: center;
}

.content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  display: flex;
}

.other-service {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  justify-items: center;
}

.other-service-button {
  display: flex;
  flex-direction: column;
}

.other-service-button .p-button-outlined:not(:last-child) {
  margin-bottom: 10px;
}

.separator {
  padding: 0 25px;
  position: relative;
}

.vertical-line {
  background-color: #dce2e6;
  width: 1px;
  height: 100%;
  position: absolute;
  left: 50%;
}

.or {
  background-color: #dce2e6;
  padding: 10px 10px;
  min-width: 48px;
  border-radius: 48px;
  font-size: 24px;
  color: white;
  text-align: center;
  display: inline-block;
  position: relative;
}

.service-subtitle {
  color: #b2bbc0;
  font-weight: 300;
  font-size: 21px;
  margin-top: 0;
}

.title {
  text-align: center;
}

h1 {
  margin: 0;
}

.form-buttons {
  display: flex;
  justify-content: left;
}
</style>
