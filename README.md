# InforceTask2.0
TECHNICAL STACK
- ASP.NET MVC (Framework or Core)
- Angular (Latest version) or React
- EntityFramework CodeFirst approach
- Any Framework for Unit tests

# ACTUAL TASK
We need to create Photo Gallery. The goals are to create an application where people will be
able to upload images, create albums, like or dislike images from other people.
There are such views: Login view, Albums table page, My Albums page, Album view page.
Adding some Unit tests will be a huge plus.

## LOGIN VIEW
On the Login View you should be able to enter Login, Password and Authorize yourself.
You need to have Admin and ordinary users.

## ALBUMS TABLE PAGE
This page has table with all Albums(table with pagination, 5 Albums on each page), cover of
the album should be the first image from this album.
Unauthorized users can see all albums.
Ordinary authorized users can see all albums.
Admin users can see and delete all albums.

## MY ALBUMS PAGE (Anonymous can't access this page)
This page contains table with user's albums. User can create new album or delete existing.

## ALBUM VIEW PAGE
This page contains table with all images from album(table with pagination, 5 images on each
page), like and dislike counters for each image.
Full sized images should be shown only when clicking on them, in all other cases it should be
thumbnails.
Unauthorized users can see all images, like and dislike counters but have no possibility to Like
or Dislike image.
Ordinary authorized users can see all images, like and dislike counters, delete their own
images and have possibility to Like or Dislike image.
Admin users can delete any image from any album.

# Guidelines
Do not spend more than three days working on this task.
If we have a technical interview, and I hope we do, we will focus on enhancing this application
and discussing how you worked through some of these problems. It's important that we see
your best work, if that means that you do not satisfy all of the requirements here. That is okay,
we don't expect everyone to finish all parts at time.
If you have to choose between refactoring and making one piece of this perfect and
implementing the next feature, choose refactoring because we want to see how your best
work looks.
We want to see clear, correct code that uses the right data structures and
patterns, and we want to see your style
