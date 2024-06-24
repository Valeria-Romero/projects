<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<title>Nueva licencia</title>
</head>
<body>
	<h2>Nueva licencia</h2>
	<form:form action="/licencia" method="POST" modelAttribute="licencia">
		<form:label path="persona">Persona</form:label>
		<form:errors path="persona"/>
		<form:select path="persona">
			<c:forEach items="${persona }" var="persona">
				<form:option value="${persona.id}"> ${persona.nombre} ${persona.apellido} </form:option>
			</c:forEach>
		</form:select>
		<form:label path="estado">Estado</form:label>
		<form:errors path="estado"/>
		<form:input path="estado"/>
		<form:label path="fechaExpiracion">Fecha de expiración</form:label>
		<form:errors path="fechaExpiracion"/>
		<form:input path="fechaExpiracion" type="date"/>
		
		<input type="submit" value="Crear licencia"/>
	</form:form>
	
	<a href="/">Home</a>
	<a href="/nueva">Nueva persona</a>
</body>
</html>