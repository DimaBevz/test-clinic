import { authThunks } from "@store/auth";
import { useGetComments } from "@hooks/comment/useGetComments";
import { useToast } from "@hooks/general/useToast";
import { useAppDispatch } from "@store/index";
import { useEffect, useRef, useState } from "react";
import useAuthUser from "@hooks/general/useAuthUser";

function useProfileController() {
  const { user } = useAuthUser();
  const { comments } = useGetComments({
    id: user.id,
  });
  const dispatch = useAppDispatch();
  const { successToast, errorToast } = useToast();
  const [rating, setRating] = useState(0);
  const hiddenFileInput = useRef<HTMLInputElement>(null);
  const handleClick = () => {
    hiddenFileInput.current?.click();
  };
  const handleChange = async (event: any) => {
    const formData = new FormData();
    formData.append("image", event.target.files[0]);
    const { payload } = await dispatch(authThunks.uploadPhoto(formData));
    if (payload?.presignedUrl) {
      successToast("Photo uploaded successfully");
    } else {
      errorToast("Error while uploading photo");
    }
  };
  useEffect(() => {
    if (user && "rating" in user) {
      setRating(user.rating);
    }
  }, [user]);

  return {
    user,
    comments,
    rating,
    hiddenFileInput,
    handleClick,
    handleChange
  };
}

export default useProfileController;
