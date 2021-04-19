<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div class="modal"
           role="dialog"
           aria-labelledby="modalTitle"
           aria-describedby="modalDescription"
      >
        <header
                class="modal-header"
                id="modalTitle"
        >
          <slot name="header">
            This is the default tile!
          </slot>
        </header>

        <section
                class="modal-body"
                id="modalDescription"
        >
          <slot name="body">
            This is the default body!
          </slot>
        </section>

        <footer class="modal-footer">
          <slot name="footer">
            This is the default footer!
          </slot>
          <button
                  type="button"
                  class="btn-modal"
                  @click="close"
                  aria-label="Close modal"
          >
            Close
          </button>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
    export default {
        name: "ModalWindow",
        methods: {
            close() {
                this.$emit('close');
            },
        },
    }
</script>

<style scoped>

  .modal-backdrop {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.7);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal {
    background: rgba(0, 0, 0, 0.38);
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25), 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 15px;
    overflow-x: auto;
    width: 1000px;
    font-family: "Montserrat", sans-serif;
    display: flex;
    border: 1px solid #4AAE9B;
    flex-direction: column;
  }

  .modal-header,
  .modal-footer {

    padding: 15px;
    display: flex;
  }

  .modal-header {
    position: relative;
    color: white;
    justify-content: space-between;
  }

  .modal-footer {
    display: flex;
    justify-content: center;
  }

  .modal-body {
    position: relative;
    padding: 8px 16px;
    margin-top: 20px;
    margin-bottom: 20px;
    display: flex;
    justify-content: space-between;
  }

  .btn-close {
    position: absolute;
    top: 0;
    right: 0;
    border: none;

    font-size: 20px;
    padding: 10px;
    cursor: pointer;
    font-weight: bold;
    color: #4AAE9B;
    background: transparent;
  }

  .btn-modal {
    color: white;
    margin-left: 10px;
    background: #4AAE9B;
    border: 1px solid #4AAE9B;
    width: 150px;
    height: 45px;
    font-size: 20px;
    background: rgba(58, 50, 50, 0.19);
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25), 0px 4px 4px rgba(0, 0, 0, 0.25), 0px 4px 4px rgba(0, 0, 0, 0.25), 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 15px;
  }
  .modal-fade-enter,
  .modal-fade-leave-to {
    opacity: 0;
  }

  .modal-fade-enter-active,
  .modal-fade-leave-active {
    transition: opacity .5s ease;
  }
</style>