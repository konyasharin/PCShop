import { useState } from 'react';

function useLike() {
  const [likes, setLikes] = useState(0);
  const [isActive, setIsActive] = useState(false);

  function setIsActiveHandle(newIsActive: boolean) {
    setIsActive(newIsActive);
    if (newIsActive) {
      setLikes(likes + 1);
    } else {
      setLikes(likes - 1);
    }
  }

  return {
    likes,
    isActive,
    setIsActiveHandle,
  };
}

export default useLike;
