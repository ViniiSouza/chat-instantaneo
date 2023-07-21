function compareDates(date1, date2) {
  return date1.toDateString() === date2.toDateString()
}

function addDays(date, days) {
  var result = new Date(date);
  result.setDate(result.getDate() + days);
  return result;
}

const padString = (string, padNumber = 2) => {
  return string.toString().padStart(padNumber, '0')
}

function getDate(dateString) {
  return new Date(dateString)
}

function getTodayDate() {
  return new Date()
}

function getYesterdayDate() {
  return addDays(getTodayDate(), -1)
}

function getStringDate(dateString) {
  var date = getDate(dateString)
  return `${padString(date.getDate())}/${padString(date.getMonth() + 1)}/${date.getFullYear()}`
}

function getStringTime(dateString) {
  var date = getDate(dateString)
  return `${padString(date.getHours())}:${padString(date.getMinutes())}`
}

export default {
  getDate,
  dateIsToday: dateString => {
    return compareDates(getDate(dateString), getTodayDate())
  },
  dateIsYesterday: dateString => {
    return compareDates(getDate(dateString), getYesterdayDate())
  },
  getStringDate,
  getStringTime,
  padString
}