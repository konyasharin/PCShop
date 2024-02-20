import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { addLike, deleteLike } from '../api/likes.ts';
import { setIsLoading } from '../store/slices/loadingSlice.ts';

function useLike() {
  const [likes, setLikes] = useState(0);
  const [isActive, setIsActive] = useState(false);
  const dispatch = useDispatch();

  function setIsActiveHandle(newIsActive: boolean) {
    dispatch(setIsLoading(true));
    if (newIsActive) {
      addLike().then(() => {
        setLikes(likes + 1);
        dispatch(setIsLoading(false));
        setIsActive(newIsActive);
      });
    } else {
      deleteLike().then(() => {
        setLikes(likes - 1);
        dispatch(setIsLoading(false));
        setIsActive(newIsActive);
      });
    }
  }

  return {
    likes,
    isActive,
    setIsActiveHandle,
  };
}

export default useLike;
