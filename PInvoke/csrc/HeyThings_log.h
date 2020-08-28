#ifndef HT_LOG_H
#define HT_LOG_H

#define HT_LOG_LEVEL

#define EXPORT

typedef enum {
	HT_LOG_VERBOSE,
	HT_LOG_DEBUG,
	HT_LOG_INFO,
	HT_LOG_WARNING,
	HT_LOG_ERROR
}ht_log_level_e;

EXPORT void ht_log_set_level(ht_log_level_e level);
EXPORT int ht_log_level();
EXPORT int ht_log_set_file(char *path);
EXPORT void ht_debug_hex(void *buf, int len);
#ifndef ht_log_verbose
#define ht_log_verbose(fmt, ...) ht_log_printf(HT_LOG_VERBOSE, fmt, ## __VA_ARGS__)
#endif

#ifndef ht_log_debug
#define ht_log_debug(fmt, ...) ht_log_printf(HT_LOG_DEBUG, fmt, ## __VA_ARGS__)
#endif

#ifndef ht_log_info
#define ht_log_info(fmt, ...) ht_log_printf(HT_LOG_INFO, fmt, ## __VA_ARGS__)
#endif

#ifndef ht_log_warn
#define ht_log_warn(fmt, ...) ht_log_printf(HT_LOG_WARNING, fmt,## __VA_ARGS__)
#endif

#ifndef ht_log_err
#define ht_log_err(fmt, ...) ht_log_printf(HT_LOG_ERROR, fmt, ## __VA_ARGS__)
#endif

void ht_log_printf(ht_log_level_e log_level, const char *fmt, ...);

#endif
