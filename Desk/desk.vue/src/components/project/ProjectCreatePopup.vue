<script lang="ts">
import { defineComponent } from "vue";
import { BaseHttp } from "@/helpers/BaseHttp";
import { CreateRequest } from "./requests/createRequest";

import useVuelidate from "@vuelidate/core";
import { required, maxLength } from "@vuelidate/validators";

export default defineComponent({
  name: "project-create-popup",

  setup() {
    return { v$: useVuelidate() };
  },

  emits: ["open", "close", "created"],

  props: {
    visible: {
      type: Boolean,
      required: true,
    },
  },

  data: function () {
    return {
      request: {
        id: 0,
        name: "",
        description: "",
      } as CreateRequest,

      isOpened: this.visible as boolean,
    };
  },

  validations() {
    return {
      request: {
        id: 0,
        name: { required },
        description: { maxLengthValue: maxLength(1024) },
      },
    };
  },

  methods: {
    cancelDialog(): void {
      this.isOpened = false;
    },

    afterHidden() {
      this.$emit("close");

      this.request = {
        id: 0,
        name: "",
        description: "",
      };

      this.v$.$reset();
    },

    async create(): Promise<void> {
      const isFormCorrect = await this.v$.$validate();

      if (isFormCorrect) {
        try {
          const response = await BaseHttp.post("Project/Create", this.request);

          if (response.status === 200) {
            this.$emit("created");
            this.isOpened = false;
          }
        } catch (error) {
          console.log(error);
        }
      }
    },
  },

  watch: {
    visible() {
      this.isOpened = this.visible;
    },
  },
});
</script>

<template>
  <p-dialog
    header="New Project"
    v-model:visible="isOpened"
    :modal="true"
    position="top"
    :draggable="false"
    @after-hide="afterHidden"
    @show="this.$emit('open')"
  >
    <form class="form" @submit.prevent>
      <div class="grid">
        <div class="field col">
          <input-text
            label="Name:"
            v-model.trim="request.name"
            placeholder="Write name"
            :validation="v$.request.name"
          />
        </div>
      </div>
      <div class="grid">
        <div class="field col">
          <text-area
            label="Description:"
            v-model.trim="request.description"
            placeholder="Write description"
            :validation="v$.request.description"
            :autoResize="false"
          />
        </div>
      </div>

      <div class="form-buttons">
        <p-button
          class="p-button-outlined p-button-danger"
          label="Cancel"
          @click="cancelDialog"
        />
        <p-button class="p-button-outlined" label="Create" @click="create" />
      </div>
    </form>
  </p-dialog>
</template>

<style scoped>
.form {
  margin: 0;
  padding: 0;
  width: 480px;
}

.form-buttons {
  padding: 0.5em;
  display: flex;
  justify-content: flex-end;
  align-items: flex-end;
}

.form-buttons .p-button-outlined:not(:first-child) {
  margin-left: 10px;
}

.link-name i {
  margin-right: 8px;
}
</style>
