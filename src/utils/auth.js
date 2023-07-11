function parseJwt(token) {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(
    atob(base64)
      .split('')
      .map((c) => `%${`00${c.charCodeAt(0).toString(16)}`.slice(-2)}`)
      .join('')
  );

  return JSON.parse(jsonPayload);
}

export default {
  isLoggedIn: () => {
    const token = localStorage.getItem('token');  
    if (token) {
      const decodedToken = parseJwt(token);
      const currentTime = Date.now() / 1000;
      if (decodedToken.exp && decodedToken.exp > currentTime) {
        return true; // valid token (not expired)
      }
    }
  
    return false; // no token/invalid token
  }
}