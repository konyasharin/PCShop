const data = {
  status: 'test',
};

export function addLike() {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve({ ...data });
    }, 1000);
  });
}

export function deleteLike() {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve({ ...data });
    }, 1000);
  });
}
