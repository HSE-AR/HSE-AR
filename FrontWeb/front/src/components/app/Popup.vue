<template>
    <div class="popup">
        <div class="popup__header">
            <span>Popup name</span>
        </div>
        <div class="popup__content">
            <slot></slot>
        </div>
        <div class="popup__footer">
            <button @click="closePopup" class="close__modal">Close</button>
            <button class="submit__button">Submit</button>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Popup",
        data() {
            return {}
        },
        methods: {
            closePopup() {
                this.$emit('closePopup')
            }
        }
    }
</script>

<style lang="scss" scoped>
    .popup {
        padding: 16px;
        position: fixed;
        top: 50px;
        width: 400px;
        color: black;
        background: white;
        box-shadow: 0 0 17px 0 #e7e7e7;
        &__header, &__footer {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        &__content {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .submit__button {
            padding: 8px;
            color: white;
            background: black;
        }
        .close__modal {
            padding: 8px;
            background: red;
            color: white;
        }

    }

</style>