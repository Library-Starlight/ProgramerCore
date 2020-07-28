#ifndef HT_MEM_H
#define HT_MEM_H

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h>

typedef struct ht_allocators_s {
	void *(*f_malloc)(size_t);
	void *(*f_realloc)(void *, size_t);
	void (*f_free)(void *);
}ht_allocators_t;

extern ht_allocators_t _ht_allocators;

void ht_set_allocators(void *(*f_malloc)(size_t),
		void *(*f_realloc)(void *, size_t),
		void (*f_free)(void *));

void *ht_malloc(size_t size);
void *ht_malloc0(size_t size);

void *ht_realloc(void *ptr, size_t size);

void *ht_calloc(size_t count, size_t size);
void *ht_calloc0(size_t count, size_t size);

void ht_free(void *ptr);

#define ht_xfree(ptr) do{if(ptr)ht_free(ptr);ptr=NULL;}while(0)

#endif
