import { useState } from "react";

function useChangePasswordController() {
  const [showPassword, setShowPassword] = useState({
    oldPassword: false,
    newPassword: false,
  });

  const handleClickShowPassword = (name: string) => {
    switch (name) {
      case "oldPass":
        setShowPassword({
          ...showPassword,
          oldPassword: !showPassword.oldPassword,
        });
        break;
      case "newPass":
        setShowPassword({
          ...showPassword,
          newPassword: !showPassword.newPassword,
        });
        break;
      default:
        break;
    }
  };

  return {
    showPassword,
    handleClickShowPassword
  }
}

export default useChangePasswordController;
