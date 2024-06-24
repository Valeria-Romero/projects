<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<title>Licencia de manejo</title>
</head>
<body>
	<h1>Nueva persona</h1>
	<form:form action="/persona" method="POST" modelAttribute="persona">
		<div>
			<form:label path="nombre">Nombre</form:label>
			<form:errors path="nombre"/>
			<form:input path="nombre"/>
		</div>
		<div>
			<form:label path="apellido">Apellido</form:label>
			<form:errors path="apellido"/>
			<form:input path="apellido"/>
		</div>
		<input type="submit" value="Guardar persona"/>
	</form:form>
</body>
</html>